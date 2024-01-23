using Bogus;
using FluentAssertions;
using FIAP.TechChalenge.InvestNetHub.Domain.Validation;
using FIAP.TechChalenge.InvestNetHub.Domain.Exceptions;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Domain.Validation;
public class DomainValidationTest
{
    private Faker Faker { get; set; } = new Faker("pt_BR");

    [Fact(DisplayName = nameof(NotNullOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullOk()
    {
        var value = Faker.Name.Random.String2(10);

        Action action =
            () => DomainValidation.NotNull(value, "Value");

        action.Should().NotThrow();
    }

    [Fact(DisplayName = nameof(NotNullThrowException))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullThrowException()
    {
        string? value = null;

        Action action =
            () => DomainValidation.NotNull(value, "FieldName");

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("FieldName cannot be null.");
    }

    [Fact(DisplayName = nameof(NotNullOrEmptyOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void NotNullOrEmptyOk()
    {
        var value = Faker.Name.Random.String2(10);

        Action action =
            () => DomainValidation.NotNullOrEmpty(value, "Value");

        action.Should().NotThrow();
    }

    [Theory(DisplayName = nameof(NotNullOrEmptyThrowException))]
    [Trait("Domain", "DomainValidation - Validation")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void NotNullOrEmptyThrowException(string? target)
    {
        Action action =
            () => DomainValidation.NotNullOrEmpty(target, "FieldName");

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("FieldName cannot be empty or null.");
    }

    [Fact(DisplayName = nameof(DateNotInFutureOk))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void DateNotInFutureOk()
    {
        var publishDate = Faker.Date.Past();

        Action action =
            () => DomainValidation.DateNotInFuture(publishDate, "FieldName");

        action.Should().NotThrow();
    }

    [Fact(DisplayName = nameof(DateNotInFutureThrowException))]
    [Trait("Domain", "DomainValidation - Validation")]
    public void DateNotInFutureThrowException()
    {
        var publishDate = Faker.Date.Future();

        Action action =
            () => DomainValidation.DateNotInFuture(publishDate, "FieldName");

        action.Should()
            .Throw<EntityValidationException>()
            .WithMessage("FieldName date cannot be in the future.");
    }
}
