using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.Common.Enums;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using UseCase = FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.UpdateAnalysisResults;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.User.UpdateUserAnalysisResults;
[Collection(nameof(UpdateUserAnalysisResultsTestFixture))]
public class UpdateUserAnalysisResultsTest
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly UseCase.UpdateUserAnalysisResults _useCase;
    private readonly UpdateUserAnalysisResultsTestFixture _fixture;

    public UpdateUserAnalysisResultsTest(UpdateUserAnalysisResultsTestFixture fixture)
    {
        _fixture = fixture;
        _userRepository = new Mock<IUserRepository>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _useCase = new UseCase.UpdateUserAnalysisResults(
            _userRepository.Object, 
            _unitOfWork.Object,
            Mock.Of<ILogger<UseCase.UpdateUserAnalysisResults>>());
    }

    [Fact(DisplayName = nameof(HandleWhenSuccededAnalysis))]
    [Trait("Application", "UpdateAnalysisResults - Use Cases")]
    public async Task HandleWhenSuccededAnalysis()
    {
        var exampleUser = _fixture.GetValidUser();
        var input = _fixture.GetSucceededAnalysisInput(exampleUser.Id);
        _userRepository.Setup(x => x.Get(
                exampleUser.Id, 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(exampleUser);

        UserModelOutput output = await _useCase.Handle(input, CancellationToken.None);

        output.Should().NotBeNull();
        output.Id.Should().Be(exampleUser.Id);
        output.Name.Should().Be(exampleUser.Name);
        output.Email.Should().Be(exampleUser.Email);
        output.Phone.Should().Be(exampleUser.Phone);
        output.CPF.Should().Be(exampleUser.CPF);
        output.DateOfBirth.Should().Be(exampleUser.DateOfBirth);
        output.RG.Should().Be(exampleUser.RG);
        output.IsActive.Should().Be(exampleUser.IsActive);

        exampleUser.AnalysisStatus.Should().Be(AnalysisStatus.Completed);
        exampleUser.RiskLevel.Should().Be(input.RiskLevel);
        exampleUser.InvestmentPreferences.Should().Be(input.InvestmentPreferences);
        exampleUser.AnalysisDate.Should().Be(input.AnalysisDate);
        
        _userRepository.VerifyAll();
        _userRepository.Verify(x => x.Update(
            exampleUser, It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWork.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = nameof(HandleWhenFailedAnalysis))]
    [Trait("Application", "UpdateAnalysisResults - Use Cases")]
    public async Task HandleWhenFailedAnalysis()
    {
        var exampleUser = _fixture.GetValidUser();
        var input = _fixture.GetFailedAnalysisInput(exampleUser.Id);
        _userRepository.Setup(x => x.Get(
                exampleUser.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(exampleUser);

        UserModelOutput output = await _useCase.Handle(input, CancellationToken.None);

        exampleUser.AnalysisStatus.Should().Be(AnalysisStatus.Error);        
        exampleUser.RiskLevel.Should().Be(input.RiskLevel);
        exampleUser.InvestmentPreferences.Should().Be(input.InvestmentPreferences);

        _userRepository.VerifyAll();
        _userRepository.Verify(x => x.Update(
            exampleUser, It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWork.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact(DisplayName = nameof(HandleWhenInvalidInput))]
    [Trait("Application", "UpdateAnalysisResults - Use Cases")]
    public async Task HandleWhenInvalidInput()
    {
        var exampleUser = _fixture.GetValidUser();
        var expectedAnalysisStatus = exampleUser.AnalysisStatus;
        var expectedErrorMessage = "Invalid Analysis Status";
        var input = _fixture.GetInvalidStatusInput(exampleUser.Id);
        _userRepository.Setup(x => x.Get(
                exampleUser.Id,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(exampleUser);

        var action = async() => await _useCase.Handle(input, CancellationToken.None);

        await action.Should().ThrowAsync<EntityValidationException>()
            .WithMessage(expectedErrorMessage);

        exampleUser.AnalysisStatus.Should().Be(expectedAnalysisStatus);

        _userRepository.VerifyAll();
        _userRepository.Verify(x => x.Update(
            exampleUser, It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWork.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Never);
    }

}
