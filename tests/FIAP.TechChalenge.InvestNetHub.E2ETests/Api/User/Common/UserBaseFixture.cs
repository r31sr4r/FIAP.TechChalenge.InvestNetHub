using Bogus.Extensions.Brazil;
using FIAP.TechChalenge.InvestNetHub.Domain.Events;
using FIAP.TechChalenge.InvestNetHub.E2ETests.Base;
using FIAP.TechChalenge.InvestNetHub.Infra.Messaging.JsonPolicies;
using System.Text;
using System.Text.Json;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.E2ETests.Api.User.Common;
public class UserBaseFixture
    : BaseFixture
{
    private const string UserCreatedQueue = "user.created.queue";
    private const string RoutingKey = "user.created";
    public UserPersistence Persistence { get; private set; }

    public UserBaseFixture()
        : base()
    {
        Persistence = new UserPersistence(CreateDbContext());
    }

    public void SetupRabbitMQ()
    {
        var channel  = WebAppFactory.RabbitMQChannel;
        var exchange = WebAppFactory.RabbitMQConfiguration.Exchange;
        channel.ExchangeDeclare(exchange, "direct", true, true, null);
        channel.QueueDeclare(UserCreatedQueue, true, false, false, null);
        channel.QueueBind(UserCreatedQueue, exchange, RoutingKey, null);
    }

    public void TearDownRabbitMQ()
    {
        var channel = WebAppFactory.RabbitMQChannel;
        var exchange = WebAppFactory.RabbitMQConfiguration.Exchange;
        channel.QueueUnbind(UserCreatedQueue, exchange, RoutingKey, null);
        channel.QueueDelete(UserCreatedQueue, false, false);
        channel.ExchangeDelete(UserCreatedQueue, false);    
    }   

    public (UserCreatedEvent?, uint) ReadMessageFromRabbitMQ()
    {
        var channel = WebAppFactory.RabbitMQChannel;
        var consumingResult = channel.BasicGet(UserCreatedQueue, true);
        var rawMessage = consumingResult.Body.ToArray();
        var stringMessage = Encoding.UTF8.GetString(rawMessage);
        var jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = new JsonSnakeCasePolicy()
        };
        var @event = JsonSerializer.Deserialize<UserCreatedEvent>(
            stringMessage, jsonOptions);
        return (@event, consumingResult.MessageCount);
    }

    public string GetValidUserName()
    {
        var userName = "";
        while (userName.Length < 3)
            userName = Faker.Internet.UserName();
        if (userName.Length > 255)
            userName = userName[..255];
        return userName;
    }

    public string GetValidEmail()
        => Faker.Internet.Email();

    public string GetValidPhone()
    {
        var phoneNumber = Faker.Random.Bool()
            ? Faker.Phone.PhoneNumber("(##) ####-####")
            : Faker.Phone.PhoneNumber("(##) #####-####");

        return phoneNumber;
    }

    public string GetValidCPF()
        => Faker.Person.Cpf();

    public string GetValidRG()
        => Faker.Person.Random.AlphaNumeric(9);

    public DateTime GetValidDateOfBirth()
        => Faker.Person.DateOfBirth;

    public string GetValidPassword()
        => "ValidPassword123!";

    public bool GetRandomBoolean()
    => new Random().NextDouble() < 0.5;

    public DomainEntity.User GetValidUser()
        => new(
            GetValidUserName(),
            GetValidEmail(),
            GetValidPhone(),
            GetValidCPF(),
            GetValidDateOfBirth(),
            GetValidRG(),
            GetValidPassword()
        );

    public DomainEntity.User GetValidUserWithoutPassword()
        => new(
            GetValidUserName(),
            GetValidEmail(),
            GetValidPhone(),
            GetValidCPF(),
            GetValidDateOfBirth(),
            GetValidRG(),
            string.Empty
        );


    public string GetInvalidEmail() => "invalid-email";

    public string GetInvalidCPF() => "invalid-cpf";
    
    public string GetInvalidRG() => "invalid-rg";

    public string GetInvalidShortName() => Faker.Internet.UserName()[..2];

    public string GetInvalidTooLongName()
    {
        var invalidTooLongName = Faker.Internet.UserName();
        while (invalidTooLongName.Length <= 255)
            invalidTooLongName = $"{invalidTooLongName} {Faker.Commerce.ProductName()}";
        return invalidTooLongName;
    }

    public List<DomainEntity.User> GeUsersList(int lenght = 10)
    {
        return Enumerable.Range(1, lenght)
            .Select(_ => GetValidUser()
        ).ToList();
    }
}
