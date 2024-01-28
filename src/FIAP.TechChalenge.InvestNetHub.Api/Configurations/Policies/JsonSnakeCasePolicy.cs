using FIAP.TechChalenge.InvestNetHub.Api.Extensios.String;
using System.Text.Json;

namespace FIAP.TechChalenge.InvestNetHub.Api.Configurations.Policies;

public class JsonSnakeCasePolicy : JsonNamingPolicy
{
    public override string ConvertName(string name)
        => name.ToSnakeCase();
}
