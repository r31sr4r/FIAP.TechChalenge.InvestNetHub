using Moq;
using UseCases = FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.CreatePortfolio;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FluentAssertions;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.Portfolio.CreatePortfolio;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.Portfolio.CreatePortfolio;

[Collection(nameof(CreatePortfolioTestFixture))]
public class CreatePortfolioTest
{
    private readonly CreatePortfolioTestFixture _fixture;

    public CreatePortfolioTest(CreatePortfolioTestFixture fixture) 
        => _fixture = fixture;

    [Fact(DisplayName = nameof(CreatePortfolio))]
    [Trait("Application", "Create Portfolio - Use Cases")]
    public async void CreatePortfolio()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

        var useCase = new UseCases.CreatePortfolio(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var input = _fixture.GetInput();

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            repository => repository.Insert(
                It.IsAny<DomainEntity.Portfolio>(),
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
        output.Description.Should().Be(input.Description);
        output.UserId.Should().Be(input.UserId);
        output.Id.Should().NotBeEmpty();
        output.CreatedAt.Should().NotBeSameDateAs(default);
    }

    [Theory(DisplayName = nameof(ThrowWhenCantInstantiatePortfolio))]
    [Trait("Application", "Create Portfolio - Use Cases")]
    [MemberData(
    nameof(CreatePortfolioTestDataGenerator.GetInvalidInputs),
    parameters: 12,
    MemberType = typeof(CreatePortfolioTestDataGenerator)
    )]
    public async void ThrowWhenCantInstantiatePortfolio(
    CreatePortfolioInput input,
    string expectionMessage
    )
    {
        var useCase = new UseCases.CreatePortfolio(
            _fixture.GetRepositoryMock().Object,
            _fixture.GetUnitOfWorkMock().Object
        );

        Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

        await task.Should()
            .ThrowAsync<EntityValidationException>()
            .WithMessage(expectionMessage);
    }
}
