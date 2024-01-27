﻿using FIAP.TechChalenge.InvestNetHub.Application.Interfaces;
using FIAP.TechChalenge.InvestNetHub.Domain.Repository;
using MediatR;

namespace FIAP.TechChalenge.InvestNetHub.Application.UseCases.User.DeleteUser;
public class DeleteUser : IDeleteUser
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUser(IUserRepository categoryRepository, IUnitOfWork unitOfWork)
        => (_userRepository, _unitOfWork) = (categoryRepository, unitOfWork);

    public async Task<Unit> Handle(DeleteUserInput request, CancellationToken cancellationToken)
    {
        var category = await _userRepository.Get(request.Id, cancellationToken);
        await _userRepository.Delete(category, cancellationToken);
        await _unitOfWork.Commit(cancellationToken);
        return Unit.Value;
    }
}