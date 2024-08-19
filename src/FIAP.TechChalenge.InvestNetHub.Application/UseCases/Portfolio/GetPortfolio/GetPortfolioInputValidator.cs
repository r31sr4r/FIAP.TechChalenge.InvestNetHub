using FluentValidation;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.GetPortfolio;
public class GetPortfolioInputValidator : AbstractValidator<GetPortfolioInput>
{
    public GetPortfolioInputValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
