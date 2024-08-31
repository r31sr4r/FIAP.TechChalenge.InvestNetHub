using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.AddAssetToPortfolio;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FluentAssertions;
using Moq;
using Xunit;
using UseCases = FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.AddAssetToPortfolio;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.AddAssetToPortfolio;

[Collection(nameof(AddAssetToPortfolioTestFixture))]
public class AddAssetToPortfolioTests
{
    private readonly AddAssetToPortfolioTestFixture _fixture;

    public AddAssetToPortfolioTests(AddAssetToPortfolioTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(AddAssetToPortfolio))]
    [Trait("Application", "AddAssetToPortfolio - Use Cases")]
    public async Task AddAssetToPortfolio()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var portfolio = _fixture.GetValidPortfolio();

        repositoryMock.Setup(x => x.Get(portfolio.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(portfolio);

        var useCase = new UseCases.AddAssetToPortfolio(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var input = _fixture.GetValidInput(portfolio.Id);

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
        output.Name.Should().Be(input.Name);
        output.Code.Should().Be(input.Code);
        output.Quantity.Should().Be(input.Quantity);
        output.Price.Should().Be(input.Price);
        output.Type.Should().Be(input.Type);
        output.Id.Should().NotBeEmpty();
    }

    [Theory(DisplayName = nameof(ThrowWhenCantInstantiateAsset))]
    [Trait("Application", "AddAssetToPortfolio - Use Cases")]
    [MemberData(
        nameof(AddAssetToPortfolioTestDataGenerator.GetInvalidInputs),
        parameters: 4,
        MemberType = typeof(AddAssetToPortfolioTestDataGenerator)
    )]
    public async Task ThrowWhenCantInstantiateAsset(
        AddAssetToPortfolioInput input,
        string expectedMessage
    )
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var portfolio = _fixture.GetValidPortfolio(input.PortfolioId);

        repositoryMock.Setup(x => x.Get(input.PortfolioId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(portfolio);

        var useCase = new UseCases.AddAssetToPortfolio(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

        await task.Should()
            .ThrowAsync<EntityValidationException>()
            .WithMessage(expectedMessage);
    }
}
