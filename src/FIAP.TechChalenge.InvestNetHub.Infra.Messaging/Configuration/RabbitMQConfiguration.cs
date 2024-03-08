﻿namespace FIAP.TechChalenge.InvestNetHub.Infra.Messaging.Configuration;
public class RabbitMQConfiguration
{
    public const string ConfigurationSection = "RabbitMQ";
    public string? HostName { get; set; }
    public int Port { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public string? Exchange { get; set; }
}
