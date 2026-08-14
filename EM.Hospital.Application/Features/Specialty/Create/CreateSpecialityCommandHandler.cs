using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Common.Contracts.Repositories;
using EM.Hospital.Application.Common.Contracts.Services.Persistence;
using EM.Hospital.Domain.Common;
using EM.Hospital.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.Specialty.Create
{
    public class CreateSpecialityCommandHandler : ICommandHandler<CreateSpecialityCommand, Result<Guid>>
    {
        private readonly ISpecialityRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        public CreateSpecialityCommandHandler(ISpecialityRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> HandlerAsync(CreateSpecialityCommand command, CancellationToken cancellationToken = default)
        {            
            var speciality = EM.Hospital.Domain.Entities.Specialty.Create(command.Name, command.Description);
            await _repo.AddAsync(speciality.Value!);
            await _unitOfWork.SaveChangeAsync();
            return Result<Guid>.Success(speciality.Value!.Id);
        }
    }
}
