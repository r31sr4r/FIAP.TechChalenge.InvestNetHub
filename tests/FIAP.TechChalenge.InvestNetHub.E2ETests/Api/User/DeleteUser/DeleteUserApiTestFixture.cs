using FIAP.TechChalenge.InvestNetHub.E2ETests.Api.User.Common;

namespace FIAP.TechChalenge.InvestNetHub.E2ETests.Api.User.DeleteUser;

[CollectionDefinition(nameof(DeleteUserApiTestFixture))]
public class DeleteUserApiTestFixtureCollection
: ICollectionFixture<DeleteUserApiTestFixture>
{ }

public class DeleteUserApiTestFixture
    : UserBaseFixture
{

}
