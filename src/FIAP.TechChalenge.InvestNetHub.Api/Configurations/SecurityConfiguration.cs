using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FIAP.TechChalenge.InvestNetHub.Api.Common.Utilities;
using Microsoft.Extensions.Configuration;

namespace FIAP.TechChalenge.InvestNetHub.Api.Configurations
{
    public static class SecurityConfiguration
    {
        public static IServiceCollection AddSecurityServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = configuration["JwtConfig:Issuer"],
                            ValidAudience = configuration["JwtConfig:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(configuration["JwtConfig:Secret"])
                            )
                        };
                    });

            return services;
        }
    }
}
