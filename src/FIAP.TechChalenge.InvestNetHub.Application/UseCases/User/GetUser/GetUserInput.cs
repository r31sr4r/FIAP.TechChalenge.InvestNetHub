using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Common;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.GetUser;
public class GetUserInput : IRequest<UserModelOutput>
{
    public GetUserInput(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}
