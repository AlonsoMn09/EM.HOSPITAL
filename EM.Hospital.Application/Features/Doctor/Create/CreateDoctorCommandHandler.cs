using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Common.Contracts.Repositories;
using EM.Hospital.Application.Common.Contracts.Services.Persistence;
using EM.Hospital.Application.Features.Specialty.Create;
using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using EM.Hospital.Domain.Entities;

namespace EM.Hospital.Application.Features.Doctor.Create
{
    public class CreateDoctorCommandHandler : ICommandHandler<CreateDoctorCommand, Result<Guid>>
    {
        private readonly IDoctorRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        public CreateDoctorCommandHandler(IDoctorRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> HandlerAsync(CreateDoctorCommand command, CancellationToken cancellationToken = default)
        {
            var doctor = EM.Hospital.Domain.Entities.Doctor.Create(command.FirstName, command.LastName, command.Email, command.Phone, command.SpecialityId);
            await _repo.AddAsync(doctor.Value!);
            await _unitOfWork.SaveChangeAsync();
            return Result<Guid>.Success(doctor.Value!.Id);
        }
    }
}
