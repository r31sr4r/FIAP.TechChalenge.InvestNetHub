using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork;
namespace FIAP.TechChalenge.InvestNetHub.Domain.Events;
public class UserCreatedEvent : DomainEvent
{
    public UserCreatedEvent(Guid userId, string cpf)
        : base()
    {
        UserId = userId;
        CPF = cpf;
    }

    public Guid UserId { get; }
    public string CPF { get; }
}
