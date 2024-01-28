using FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.User.Common;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.User.AuthUser;

[CollectionDefinition(nameof(AuthUserTestFixture))]
public class AuthUserTestFixtureCollection
    : ICollectionFixture<AuthUserTestFixture>
{ }

public class AuthUserTestFixture
    : UserUseCasesBaseFixture
{ }
