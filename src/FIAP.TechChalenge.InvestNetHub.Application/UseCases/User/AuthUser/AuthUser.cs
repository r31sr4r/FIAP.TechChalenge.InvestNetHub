using FIAP.TechChalenge.InvestNetHub.Application.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.Common.Security;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.AuthUser;
public class AuthUser : IAuthUser
{
    private readonly IUserRepository _userRepository;

    public AuthUser(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserModelOutput> Handle(AuthUserInput request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmail(request.Email, cancellationToken);
        if (user == null)
            throw new CustomAuthenticationException("Invalid email or password.");        

        if (!PasswordHasher.VerifyPasswordHash(request.Password, user.Password!))
            throw new CustomAuthenticationException("Invalid email or password.");

        return UserModelOutput.FromUser(user);
    }
}