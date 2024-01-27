using FIAP.TechChalenge.InvestNetHub.UnitTests.Application.User.Common;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.User.GetUser;

[CollectionDefinition(nameof(GetUserTestFixture))]
public class GetUserTestFixtureCollection :
    ICollectionFixture<GetUserTestFixture>
{ }

public class GetUserTestFixture
    : UserUseCasesBaseFixture
{ }
