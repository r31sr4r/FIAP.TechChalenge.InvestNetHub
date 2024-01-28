using FIAP.TechChalenge.InvestNetHub.E2ETests.Api.User.Common;

namespace FIAP.TechChalenge.InvestNetHub.E2ETests.Api.User.AuthUser;
[CollectionDefinition(nameof(AuthUserApiTestFixture))]
public class AuthUserApiTestFixtureCollection
: ICollectionFixture<AuthUserApiTestFixture>
{ }

public class AuthUserApiTestFixture
    : UserBaseFixture
{
}
