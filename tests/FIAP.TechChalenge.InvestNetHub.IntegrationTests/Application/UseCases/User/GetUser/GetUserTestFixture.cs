﻿using FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.User.Common;

namespace FIAP.TechChalenge.InvestNetHub.IntegrationTests.Application.UseCases.User.GetUser;

[CollectionDefinition(nameof(GetUserTestFixture))]
public class GetUserTestFixtureCollection
    : ICollectionFixture<GetUserTestFixture>
{ }

public class GetUserTestFixture
    : UserUseCasesBaseFixture
{ }
