using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Common;

namespace FIAP.TechChalenge.InvestNetHub.Api.Common.Utilities;

public interface IJwtTokenGenerator
{
    string GenerateJwtToken(UserModelOutput user);
}
