using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Common.Contracts.Repositories;
using EM.Hospital.Application.Common.Contracts.Services.Persistence;
using EM.Hospital.Application.Features.Specialty.Create;
using EM.Hospital.Domain.Common;
using EM.Hospital.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.DoctorSchedule.Create
{
    public class CreateDoctorScheduleCommandHandler : ICommandHandler<CreateDoctorScheduleCommand, Result<Guid>>
    {
        private readonly IDoctorScheduleRepository _repo;
        private readonly IUnitOfWork _unitOfWork;
        public CreateDoctorScheduleCommandHandler(IDoctorScheduleRepository repo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> HandlerAsync(CreateDoctorScheduleCommand command, CancellationToken cancellationToken = default)
        {
            var dateRange = DateTimeRange.Create(command.Start, command.End);
            if (!dateRange.IsSuccess)
                return Result.Failure<Guid>(dateRange.Message!);
            var doctorSchedule = EM.Hospital.Domain.Entities.DoctorSchedule.Create(command.DayOfWeek, dateRange.Value!);
            await _repo.AddAsync(doctorSchedule.Value!);
            await _unitOfWork.SaveChangeAsync();
            return Result<Guid>.Success(doctorSchedule.Value!.Id);
        }
    }
}
