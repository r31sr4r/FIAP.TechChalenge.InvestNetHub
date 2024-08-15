using FIAP.TechChalenge.InvestNetHub.Application.Exceptions;
using FluentAssertions;
using Moq;
using UseCases = FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.DeletePortfolio;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.DeletePortfolio;
[Collection(nameof(DeletePortfolioTestFixture))]
public class DeletePortfolioTest
{
    private readonly DeletePortfolioTestFixture _fixture;

    public DeletePortfolioTest(DeletePortfolioTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(DeletePortfolio))]
    [Trait("Application", "DeletePortfolio - Use Cases")]
    public async Task DeletePortfolio()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var portfolioExample = _fixture.GetValidPortfolio();

        repositoryMock.Setup(repository => repository.Get(
                    portfolioExample.Id,
                    It.IsAny<CancellationToken>())
        ).ReturnsAsync(portfolioExample);

        var input = new UseCases.DeletePortfolioInput(portfolioExample.Id);
        var useCase = new UseCases.DeletePortfolio(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            repository => repository.Get(
                portfolioExample.Id,
                It.IsAny<CancellationToken>()
            ), Times.Once
        );

        repositoryMock.Verify(
            repository => repository.Delete(
                portfolioExample,
                It.IsAny<CancellationToken>()
            ), Times.Once
        );

        unitOfWorkMock.Verify(
            unitOfWork => unitOfWork.Commit(
                It.IsAny<CancellationToken>()
            ), Times.Once
        );
    }

    [Fact(DisplayName = nameof(ThrowWhenPortfolioNotFound))]
    [Trait("Application", "DeletePortfolio - Use Cases")]
    public async Task ThrowWhenPortfolioNotFound()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var portfolioGuid = Guid.NewGuid();

        repositoryMock.Setup(repository => repository.Get(
                    portfolioGuid,
                    It.IsAny<CancellationToken>())
        ).ThrowsAsync(
            new NotFoundException($"Portfolio '{portfolioGuid}' not found")
        );

        var input = new UseCases.DeletePortfolioInput(portfolioGuid);
        var useCase = new UseCases.DeletePortfolio(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var task = async () => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>();

        repositoryMock.Verify(
            repository => repository.Get(
                portfolioGuid,
                It.IsAny<CancellationToken>()
            ), Times.Once
        );
    }
}
