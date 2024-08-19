using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.ListPortfolios;
public interface IListPortfolios
    : IRequestHandler<ListPortfoliosInput, ListPortfoliosOutput>
{
}
