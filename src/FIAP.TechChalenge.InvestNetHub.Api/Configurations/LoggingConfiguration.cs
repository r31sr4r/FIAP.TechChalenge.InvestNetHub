﻿using Serilog;

namespace FIAP.TechChalenge.InvestNetHub.Api.Configurations
{
    public static class LoggingConfiguration
    {
        public static void AddLoggingConfiguration(this IHostBuilder host)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/investnethub.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            host.UseSerilog();
        }
    }
}
