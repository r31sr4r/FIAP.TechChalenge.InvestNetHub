using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.AddTransactionToPortfolio;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FluentAssertions;
using Moq;
using UseCases = FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.AddTransactionToPortfolio;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.AddTransactionToPortfolio;

[Collection(nameof(AddTransactionToPortfolioTestFixture))]
public class AddTransactionToPortfolioTests
{
    private readonly AddTransactionToPortfolioTestFixture _fixture;

    public AddTransactionToPortfolioTests(AddTransactionToPortfolioTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(AddTransactionToPortfolio))]
    [Trait("Application", "AddTransactionToPortfolio - Use Cases")]
    public async Task AddTransactionToPortfolio()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var portfolio = _fixture.GetValidPortfolio();
        var asset = _fixture.GetValidAsset();
        portfolio.AddAsset(asset);

        repositoryMock.Setup(x => x.Get(portfolio.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(portfolio);

        var useCase = new UseCases.AddTransactionToPortfolio(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var input = _fixture.GetValidInput(portfolio.Id, asset.Id);

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            repository => repository.Get(
                portfolio.Id,
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );

        unitOfWorkMock.Verify(
            unitOfWork => unitOfWork.Commit(It.IsAny<CancellationToken>()),
            Times.Once
        );

        output.Should().NotBeNull();
        output.Quantity.Should().Be(input.Quantity);
        output.Price.Should().Be(input.Price);
        output.Type.Should().Be(input.Type);
        output.Id.Should().NotBeEmpty();
    }
}
