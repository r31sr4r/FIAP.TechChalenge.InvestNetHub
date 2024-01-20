using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Domain.Entity;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using FluentAssertions;
using Moq;
using UseCases = FIAP.TechChalenge.InvestNetHub.Application.UseCases.MarketNews.CreateMarketNews;

namespace FIAP.TechChalenge.InvestNetHub.Tests.Application.CreateMarketNews;
public class CreateMarketNewsTest
{
    [Fact(DisplayName = nameof(CreateMarketNews))]
    [Trait("Application", "CreateMarketNews - Use Cases")]
    public async void CreateMarketNews()
    {
        var repositoryMock = new Mock<IMarketNewsRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var useCase = new UseCases.CreateMarketNews(
            repositoryMock.Object, 
            unitOfWorkMock.Object
        );

        var input = new UseCases.CreateMarketNewsInput(
            "Title",
            "Summary",
            DateTime.Now,
            "Url",
            "Source",
            "ImageUrl",
            new List<string> { "Author" },
            0.5m,
            "Label"
        ); 

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
}
