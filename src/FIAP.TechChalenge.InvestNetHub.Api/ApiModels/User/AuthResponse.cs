using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Common;

namespace FIAP.TechChalenge.InvestNetHub.Api.ApiModels.User;

public class AuthResponse
{
    public string Email { get; set; }
    public string Token { get; set; }
}
