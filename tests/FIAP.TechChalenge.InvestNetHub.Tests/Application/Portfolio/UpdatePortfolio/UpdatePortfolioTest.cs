using FIAP.TechChalenge.InvestNetHub.Application.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.Common;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.UpdatePortfolio;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FluentAssertions;
using Moq;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using UseCases = FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.UpdatePortfolio;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.UpdatePortfolio;
[Collection(nameof(UpdatePortfolioTestFixture))]
public class UpdatePortfolioTest
{
    private readonly UpdatePortfolioTestFixture _fixture;

    public UpdatePortfolioTest(UpdatePortfolioTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory(DisplayName = nameof(UpdatePortfolio))]
    [Trait("Application", "UpdatePortfolio - Use Cases")]
    [MemberData(
        nameof(UpdatePortfolioTestDataGenerator.GetPortfoliosToUpdate),
        parameters: 10,
        MemberType = typeof(UpdatePortfolioTestDataGenerator)
    )]
    public async Task UpdatePortfolio(
        DomainEntity.Portfolio portfolioExample,
        UpdatePortfolioInput input
    )
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        repositoryMock.Setup(repository => repository.Get(
                        portfolioExample.Id,
                        It.IsAny<CancellationToken>())
               ).ReturnsAsync(portfolioExample);
        var useCase = new UseCases.UpdatePortfolio
            (repositoryMock.Object,
                       unitOfWorkMock.Object
                              );

        PortfolioModelOutput output = await useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);

        repositoryMock.Verify(
            repository => repository.Get(
                portfolioExample.Id,
                It.IsAny<CancellationToken>()
                ), Times.Once
        );
        repositoryMock.Verify(
            repository => repository.Update(
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
    [Trait("Application", "UpdatePortfolio - Use Cases")]
    public async Task ThrowWhenPortfolioNotFound()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        var input = _fixture.GetValidInput();
        repositoryMock.Setup(repository => repository.Get(
            input.Id,
            It.IsAny<CancellationToken>())
            ).ThrowsAsync(new NotFoundException($"Portfolio '{input.Id}' not found"));
        var useCase = new UseCases.UpdatePortfolio(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var task = async ()
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should().ThrowAsync<NotFoundException>();

        repositoryMock.Verify(
            repository => repository.Get(
                input.Id,
                It.IsAny<CancellationToken>()
                ), Times.Once
        );
    }

    [Theory(DisplayName = nameof(ThrowWhenCantUpdatePortfolio))]
    [Trait("Application", "UpdatePortfolio - Use Cases")]
    [MemberData(
    nameof(UpdatePortfolioTestDataGenerator.GetInvalidInputs),
    parameters: 12,
    MemberType = typeof(UpdatePortfolioTestDataGenerator)
)]
    public async Task ThrowWhenCantUpdatePortfolio(
        UpdatePortfolioInput input,
        string expectedMessage
    )
    {
        var portfolio = _fixture.GetValidPortfolio();
        input.Id = portfolio.Id;
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
        repositoryMock.Setup(repository => repository.Get(
                portfolio.Id,
                It.IsAny<CancellationToken>())
        ).ReturnsAsync(portfolio);
        var useCase = new UseCases.UpdatePortfolio
            (repositoryMock.Object,
            unitOfWorkMock.Object);

        var task = async ()
            => await useCase.Handle(input, CancellationToken.None);

        await task.Should()
            .ThrowAsync<EntityValidationException>()
            .WithMessage(expectedMessage);

        repositoryMock.Verify(
            repository => repository.Get(
                portfolio.Id,
                It.IsAny<CancellationToken>()
                ), Times.Once
        );
    }
}
