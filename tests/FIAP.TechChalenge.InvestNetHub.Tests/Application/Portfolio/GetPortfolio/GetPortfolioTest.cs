using FIAP.TechChalenge.InvestNetHub.Application.Exceptions;
using FluentAssertions;
using Moq;
using UseCases = FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.GetPortfolio;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.GetPortfolio;
[Collection(nameof(GetPortfolioTestFixture))]
public class GetPortfolioTest
{
    private readonly GetPortfolioTestFixture _fixture;

    public GetPortfolioTest(GetPortfolioTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = nameof(GetPortfolio))]
    [Trait("Application", "GetPortfolio - Use Cases")]
    public async Task GetPortfolio()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var examplePortfolio = _fixture.GetValidPortfolio();
        repositoryMock.Setup(repository => repository.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        )).ReturnsAsync(examplePortfolio);
        var input = new UseCases.GetPortfolioInput(examplePortfolio.Id);
        var useCase = new UseCases.GetPortfolio(repositoryMock.Object);

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(repository => repository.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        ), Times.Once);

        output.Should().NotBeNull();
        output.Name.Should().Be(examplePortfolio.Name);
        output.Description.Should().Be(examplePortfolio.Description);
        output.UserId.Should().Be(examplePortfolio.UserId);
        output.CreatedAt.Should().Be(examplePortfolio.CreatedAt);
        output.Id.Should().Be(examplePortfolio.Id);
    }

    [Fact(DisplayName = nameof(NotFoundExceptionWhenPortfolioDoesntExist))]
    [Trait("Application", "GetPortfolio - Use Cases")]
    public async Task NotFoundExceptionWhenPortfolioDoesntExist()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var exampleGuid = Guid.NewGuid();
        repositoryMock.Setup(repository => repository.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        )).ThrowsAsync(
            new NotFoundException($"Portfolio '{exampleGuid}' not found")
        );

        var input = new UseCases.GetPortfolioInput(exampleGuid);
        var useCase = new UseCases.GetPortfolio(repositoryMock.Object);

        var task = async ()
            => await useCase.Handle(input, CancellationToken.None
        );

        await task.Should().ThrowAsync<NotFoundException>();

        repositoryMock.Verify(repository => repository.Get(
            It.IsAny<Guid>(),
            It.IsAny<CancellationToken>()
        ), Times.Once);
    }
}
