using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;

namespace FIAP.TechChalenge.InvestNetHub.Domain.Validation;
public class DomainValidation
{
    public static void NotNull(object? target, string name)
    {
        if (target is null)
            throw new EntityValidationException($"{name} cannot be null.");
    }

    public static void NotNullOrEmpty(string? target, string name)
    {
        if (string.IsNullOrWhiteSpace(target))
            throw new EntityValidationException($"{name} cannot be empty or null.");
    }

    public static void DateNotInFuture(DateTime target, string name)
    {
        if (target > DateTime.Now)
            throw new EntityValidationException($"{name} date cannot be in the future.");
    }


}
