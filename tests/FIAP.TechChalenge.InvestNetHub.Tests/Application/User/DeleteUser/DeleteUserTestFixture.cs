using FIAP.TechChalenge.InvestNetHub.UnitTests.Application.User.Common;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.User.DeleteUser;

[CollectionDefinition(nameof(DeleteUserTestFixture))]
public class DeleteUserTestFixtureCollection
    : ICollectionFixture<DeleteUserTestFixture>
{ }
public class DeleteUserTestFixture
    : UserUseCasesBaseFixture
{ }