using Moq;
using UseCases = FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.RemoveTransactionFromPortfolio;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.RemoveTransactionFromPortfolio;

[Collection(nameof(RemoveTransactionFromPortfolioTestFixture))]
public class RemoveTransactionFromPortfolioTests
{
    private readonly RemoveTransactionFromPortfolioTestFixture _fixture;

    public RemoveTransactionFromPortfolioTests(RemoveTransactionFromPortfolioTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(RemoveTransactionFromPortfolio))]
    [Trait("Application", "RemoveTransactionFromPortfolio - Use Cases")]
    public async Task RemoveTransactionFromPortfolio()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var portfolio = _fixture.GetValidPortfolio();
        var asset = _fixture.GetValidAsset(); 
        portfolio.AddAsset(asset);           
        var transaction = _fixture.GetValidTransaction(portfolio.Id, asset.Id); 
        portfolio.AddTransaction(transaction); 

        repositoryMock.Setup(x => x.Get(portfolio.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(portfolio);

        var useCase = new UseCases.RemoveTransactionFromPortfolio(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var input = _fixture.GetValidInput(portfolio.Id, transaction.Id);

        await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            repository => repository.RemoveTransactionAsync(
                portfolio.Id,
                transaction.Id,
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );

        unitOfWorkMock.Verify(
            unitOfWork => unitOfWork.Commit(It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    

}
