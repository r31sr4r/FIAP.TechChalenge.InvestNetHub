using FIAP.TechChalenge.InvestNetHub.Infra.Messaging.Extensions.String;
using System.Text.Json;

namespace FIAP.TechChalenge.InvestNetHub.Infra.Messaging.JsonPolicies;

public class JsonSnakeCasePolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
        => name.ToSnakeCase();
}
