﻿using Bogus;
using Bogus.DataSets;
using Bogus.Extensions.Brazil;
using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using FIAP.TechChalenge.InvestNetHub.UnitTests.Common;
using Moq;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.UnitTests.Application.User.Common;
public class UserUseCasesBaseFixture
    : BaseFixture
{    public Mock<IUserRepository> GetRepositoryMock() => new();

    public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();

    public string GetValidUserName()
    {
        var userName = "";
        while (userName.Length < 3)
            userName = Faker.Person.FullName;
        if (userName.Length > 255)
            userName = userName[..255];
        return userName;
    }

    public string GetValidEmail() 
        => Faker.Internet.Email();

    public string GetValidPhone()
    {
        var phoneNumber = Faker.Random.Bool()
            ? Faker.Phone.PhoneNumber("(##) ####-####")
            : Faker.Phone.PhoneNumber("(##) #####-####");

        return phoneNumber;
    }

    public string GetValidCPF()
        => Faker.Person.Cpf();

    public string GetValidRG()
        => Faker.Person.Random.AlphaNumeric(9);

    public DateTime GetValidDateOfBirth()
        => Faker.Person.DateOfBirth;

    public string GetValidPassword()
        => "ValidPassword123!";

    public bool getRandomBoolean()
    => new Random().NextDouble() < 0.5;

    public DomainEntity.User GetValidUser()
        => new(
            GetValidUserName(),
            GetValidEmail(),
            GetValidPhone(),
            GetValidCPF(),
            GetValidDateOfBirth(),
            GetValidRG(),
            GetValidPassword()
        );

    public DomainEntity.User GetValidUserWithoutPassword()
        => new(
            GetValidUserName(),
            GetValidEmail(),
            GetValidPhone(),
            GetValidCPF(),
            GetValidDateOfBirth(),
            GetValidRG()
        );

    public List<DomainEntity.User> GeUsersList(int lenght = 10)
    {
        return Enumerable.Range(1, lenght)
            .Select(_ => GetValidUser()
        ).ToList();
    }
}
