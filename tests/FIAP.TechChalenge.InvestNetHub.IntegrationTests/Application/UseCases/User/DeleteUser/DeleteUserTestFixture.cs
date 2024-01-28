using FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.User.Common;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.User.DeleteUser;
[CollectionDefinition(nameof(DeleteUserTestFixture))]
public class DeleteUserTestFixtureCollection
    : ICollectionFixture<DeleteUserTestFixture>
{ }

public class DeleteUserTestFixture
    : UserUseCasesBaseFixture
{ }
