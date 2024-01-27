﻿using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.Common;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using DomainEntity = FIAP.TechChalenge.InvestNetHub.Domain.Entity;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.CreateUser;
public class CreateUser : ICreateUser
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUser(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork
    )
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserModelOutput> Handle(CreateUserInput request, CancellationToken cancellationToken)
    {
        var user = new DomainEntity.User(
            request.Name,
            request.Email,
            request.Phone,
            request.CPF,
            request.DateOfBirth,
            request.RG,
            request.Password
        );

        await _userRepository.Insert(user, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);

        return UserModelOutput.FromUser(user);
    }
}
