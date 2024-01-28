﻿using Bogus.Extensions.Brazil;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.CreateUser;
using FIAP.TechChalenge.InvestNetHub.E2ETests.Base;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.E2ETests.Api.User.Common;
public class UserBaseFixture
    : BaseFixture
{

    public UserPersistence Persistence;

    public UserBaseFixture()
        : base()
    {
        Persistence = new UserPersistence(CreateDbContext());
    }

    public string GetValidUserName()
    {
        var userName = "";
        while (userName.Length < 3)
            userName = Faker.Internet.UserName();
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

    public bool GetRandomBoolean()
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
            GetValidRG(),
            string.Empty
        );


    public string GetInvalidEmail() => "invalid-email";

    public string GetInvalidCPF() => "invalid-cpf";
    
    public string GetInvalidRG() => "invalid-rg";

    public string GetInvalidShortName() => Faker.Internet.UserName()[..2];

    public string GetInvalidTooLongName()
    {
        var invalidTooLongName = Faker.Internet.UserName();
        while (invalidTooLongName.Length <= 255)
            invalidTooLongName = $"{invalidTooLongName} {Faker.Commerce.ProductName()}";
        return invalidTooLongName;
    }

    public List<DomainEntity.User> GeUsersList(int lenght = 10)
    {
        return Enumerable.Range(1, lenght)
            .Select(_ => GetValidUser()
        ).ToList();
    }
}
