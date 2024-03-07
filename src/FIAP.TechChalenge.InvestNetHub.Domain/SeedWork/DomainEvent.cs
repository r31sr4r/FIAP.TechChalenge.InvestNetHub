namespace FIAP.TechChalenge.InvestNetHub.Domain.SeedWork;
public abstract class DomainEvent
{
    public DateTime OccurredOn { get; set; }

    protected DomainEvent()
    {
        OccurredOn = DateTime.Now;
    }
}
