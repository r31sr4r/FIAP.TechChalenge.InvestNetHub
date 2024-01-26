﻿using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace FIAP.TechChalenge.InvestNetHub.Infra.ExternalServices.Common
{
    public abstract class ApiClientBase
    {
        protected HttpClient Client;
        protected string? ApiKey;
        protected string? BaseUrl;
        protected string? NewsFunction;

        protected ApiClientBase(string providerName)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory, "Infra.ExternalServices", "Configurations"))
                .AddJsonFile("apiSettings.json", optional: false)
                .Build();

            var providerSection = configuration.GetSection($"ExternalServices:{providerName}");

            Client = new HttpClient();
            ApiKey = providerSection["ApiKey"];
            BaseUrl = providerSection["BaseUrl"];
            NewsFunction = providerSection["NewsFunction"];
        }
    }
}
