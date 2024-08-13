namespace FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
public class BusinessRuleException : Exception
{
    public BusinessRuleException(string? message) : base(message)
    {

    }
}
