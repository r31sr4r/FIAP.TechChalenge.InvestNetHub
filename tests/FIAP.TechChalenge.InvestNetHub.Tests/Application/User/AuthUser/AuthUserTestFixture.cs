using FIAP.TechChalenge.InvestNetHub.UnitTests.Application.User.Common;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.User.AuthUser;

[CollectionDefinition(nameof(AuthUserTestFixture))]
public class AuthUserTestFixtureCollection :
    ICollectionFixture<AuthUserTestFixture>
{ }

public class AuthUserTestFixture
    : UserUseCasesBaseFixture
{ }
