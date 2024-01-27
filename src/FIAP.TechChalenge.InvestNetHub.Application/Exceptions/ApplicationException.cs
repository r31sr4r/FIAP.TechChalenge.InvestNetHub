namespace FIAP.TechChalenge.InvestNetHub.Application.Exceptions;
public abstract class ApplicationException : Exception
{
    public ApplicationException(string? message) : base(message)
    { }

}

