using FluentValidation;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.UpdatePortfolio;

public class UpdatePortfolioInputValidator
    : AbstractValidator<UpdatePortfolioInput>
{
    public UpdatePortfolioInputValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id must not be empty");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name must not be empty");
    }
}
