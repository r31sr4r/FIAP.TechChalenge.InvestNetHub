using FIAP.TechChalenge.InvestNetHub.Application.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.UpdateAnalysisResults;
using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Infra.Messaging.Configuration;
using FIAP.TechChalenge.InvestNetHub.Infra.Messaging.DTOs;
using FIAP.TechChalenge.InvestNetHub.Infra.Messaging.JsonPolicies;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace FIAP.TechChalenge.InvestNetHub.Infra.Messaging.Consumer;
public class UserAnalysisResultEventConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<UserAnalysisResultEventConsumer> _logger;
    private readonly string _queue;
    private readonly IModel _channel;

    public UserAnalysisResultEventConsumer(
        IServiceProvider serviceProvider, 
        ILogger<UserAnalysisResultEventConsumer> logger, 
        IOptions<RabbitMQConfiguration> configuration, 
        IModel channel)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _queue = configuration.Value!.UserAnalysisResultQueue!;
        _channel = channel;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += OnMessageReceived;
        _channel.BasicConsume(
            queue: _queue, 
            autoAck: false, 
            consumer: consumer);

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("UserAnalysisResultEventConsumer running at: {time}", DateTimeOffset.Now);
            await Task.Delay(100000, stoppingToken);
        }
        _logger.LogWarning("UserAnalysisResultEventConsumer is stopping.");
        _channel.Dispose();
    }

    private void OnMessageReceived(
        object? sender, 
        BasicDeliverEventArgs eventArgs
    )
    {
        var messageString = string.Empty;
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var rawMessage = eventArgs.Body.ToArray();
            messageString = Encoding.UTF8.GetString(rawMessage);
            _logger.LogDebug("Received message: {message}", messageString);
            var jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = new JsonSnakeCasePolicy()
            };
            var message = JsonSerializer
                .Deserialize<UserAnalysisResultMessageDTO>(messageString, jsonOptions);
            var input = GetUserAnalysisResultsInput(message!);

            mediator.Send(input, CancellationToken.None).Wait();
            _channel.BasicAck(eventArgs.DeliveryTag, false);
        }
        catch (Exception ex) 
            when (ex is EntityValidationException or NotFoundException)
        {
            _logger.LogError(ex, 
                "There was a business error on message processing: {deliveryTag}, {message}",
                eventArgs.DeliveryTag, messageString);
            _channel.BasicNack(eventArgs.DeliveryTag, false, false);
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex,
                "JSON deserialization error on message processing: {deliveryTag}, {message}",
                eventArgs.DeliveryTag, messageString);
            _channel.BasicNack(eventArgs.DeliveryTag, false, false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "There was a unexpected error on message processing: {deliveryTag}, {message}",
                eventArgs.DeliveryTag, messageString);
            _channel.BasicNack(eventArgs.DeliveryTag, false, true);
        }

    }

    private UpdateUserAnalysisResultsInput GetUserAnalysisResultsInput(
               UserAnalysisResultMessageDTO message)
    {
        if (message!.User != null)
        {
            bool success = Enum.TryParse<RiskLevel>(message.User.RiskLevel, true, out RiskLevel riskLevel);
            if (!success)
                riskLevel = RiskLevel.Undefined;

            return new UpdateUserAnalysisResultsInput(
                Guid.Parse(message.User.ResourceId!),
                AnalysisStatus.Completed,
                riskLevel,
                message.User.InvestmentPreferences,
                AnalysisDate: message.UpdatedAt
            );
        }     
        
        return new UpdateUserAnalysisResultsInput(
            Guid.Parse(message.Message!.ResourceId!),
            AnalysisStatus.Error,
            RiskLevel.Undefined,
            null,
            AnalysisDate: message.UpdatedAt,
            ErrorMessage: message.Error
        );
    }
}
