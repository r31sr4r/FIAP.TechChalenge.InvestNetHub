using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.CreateMarketNews;
using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using FluentAssertions;
using Moq;
using UseCases = FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.CreateMarketNews;

namespace FIAP.TechChalenge.InvestNetHub.Tests.Application.CreateMarketNews;

[Collection(nameof(CreateMarketNewsTestFixture))]
public class CreateMarketNewsTest
{
    private readonly CreateMarketNewsTestFixture _fixture;

    public CreateMarketNewsTest(CreateMarketNewsTestFixture fixture)
        => _fixture = fixture;

    [Fact(DisplayName = nameof(CreateMarketNews))]
    [Trait("Application", "CreateMarketNews - Use Cases")]
    public async void CreateMarketNews()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

        var useCase = new UseCases.CreateMarketNews(
            repositoryMock.Object,
            unitOfWorkMock.Object
        );

        var input = _fixture.GetInput();

        var output = await useCase.Handle(input, CancellationToken.None);

        repositoryMock.Verify(
            repository => repository.Insert(
                It.IsAny<MarketNews>(), 
                It.IsAny<CancellationToken>()
            ),
            Times.Once
        );

        unitOfWorkMock.Verify(
            unitOfWork => unitOfWork.Commit(It.IsAny<CancellationToken>()),
            Times.Once
        );

        output.Should().NotBeNull();
        (output.Id != Guid.Empty).Should().BeTrue();
        output.Title.Should().Be(input.Title);
        output.Summary.Should().Be(input.Summary);
        output.PublishDate.Should().Be(input.PublishDate);
        output.Url.Should().Be(input.Url);
        output.Source.Should().Be(input.Source);
        output.ImageUrl.Should().Be(input.ImageUrl);
        output.Authors.Should().BeEquivalentTo(input.Authors);
        output.OverallSentimentScore.Should().Be(input.OverallSentimentScore);
        output.OverallSentimentLabel.Should().Be(input.OverallSentimentLabel);

    }

    [Theory(DisplayName = nameof(ThrowWhenCantInstantiate))]
    [Trait("Application", "CreateMarketNews - Use Cases")]
    [MemberData(
        nameof(CreateMarketNewsTestDataGenerator.GetInvalidInputs),
        MemberType = typeof(CreateMarketNewsTestDataGenerator)
    )]
    public async void ThrowWhenCantInstantiate(
        CreateMarketNewsInput input,
        string exceptionMessage
    )
    {
        var useCase = new UseCases.CreateMarketNews(
            _fixture.GetRepositoryMock().Object,
            _fixture.GetUnitOfWorkMock().Object
        );

        Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

        await task.Should()
                .ThrowAsync<EntityValidationException>()
                .WithMessage(exceptionMessage);
    }
}
