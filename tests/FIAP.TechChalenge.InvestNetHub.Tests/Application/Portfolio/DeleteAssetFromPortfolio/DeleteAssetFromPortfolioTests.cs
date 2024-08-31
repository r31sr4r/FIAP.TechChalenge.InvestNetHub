using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.DeleteAssetFromPortfolio;
using Moq;
using Xunit;
using UseCases = FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.DeleteAssetFromPortfolio;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FluentAssertions;
using FIAP.TechChalenge.InvestNetHub.Application.Exceptions;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.DeleteAssetFromPortfolio;

[Collection(nameof(DeleteAssetFromPortfolioTestFixture))]
public class DeleteAssetFromPortfolioTests
{
    private readonly DeleteAssetFromPortfolioTestFixture _fixture;

    public DeleteAssetFromPortfolioTests(DeleteAssetFromPortfolioTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(DeleteAssetFromPortfolio))]
    [Trait("Application", "DeleteAssetFromPortfolio - Use Cases")]
    public async Task DeleteAssetFromPortfolio()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var portfolio = _fixture.GetValidPortfolio(Guid.NewGuid());
        var asset = _fixture.GetValidAsset(portfolio.Id);
        portfolio.AddAsset(asset);

        repositoryMock.Setup(x => x.Get(portfolio.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(portfolio);

        var useCase = new UseCases.DeleteAssetFromPortfolio(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var input = _fixture.GetValidInput(portfolio.Id, asset.Id);

        await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            repository => repository.RemoveAssetAsync(
                portfolio.Id,
                asset.Id,
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );

        unitOfWorkMock.Verify(
            unitOfWork => unitOfWork.Commit(It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Fact(DisplayName = nameof(ThrowWhenAssetNotFound))]
    [Trait("Application", "DeleteAssetFromPortfolio - Use Cases")]
    public async Task ThrowWhenAssetNotFound()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var portfolio = _fixture.GetValidPortfolio(Guid.NewGuid());
        var nonExistentAssetId = Guid.NewGuid();

        repositoryMock.Setup(x => x.Get(
            portfolio.Id,
            It.IsAny<CancellationToken>())
        ).ReturnsAsync(portfolio);

        repositoryMock.Setup(x => x.RemoveAssetAsync(
            portfolio.Id,
            nonExistentAssetId,
            It.IsAny<CancellationToken>())
        ).ThrowsAsync(
            new NotFoundException($"Asset with id {nonExistentAssetId} not found in portfolio {portfolio.Id}")
        );

        var useCase = new UseCases.DeleteAssetFromPortfolio(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var input = _fixture.GetValidInput(portfolio.Id, nonExistentAssetId);

        var task = async () => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>();

        repositoryMock.Verify(
            repository => repository.RemoveAssetAsync(
                portfolio.Id,
                nonExistentAssetId,
                It.IsAny<CancellationToken>()
            ), Times.Once
        );
    }
}
