using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Common.Contracts.Repositories;
using EM.Hospital.Application.Common.Contracts.Services.Persistence;
using EM.Hospital.Application.Features.Specialty.Create;
using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.Prescription.Create
{
    public class CreatePrescriptionCommandHandler : ICommandHandler<CreatePrescriptionCommand, Result>
    {
        private readonly IPrescriptionRepository _repo;
        private readonly IAppointmentRepository _appointmentRepo;
        private readonly IUnitOfWork _unitOfWork;
        public CreatePrescriptionCommandHandler(IPrescriptionRepository repo, IAppointmentRepository appointmentRepo, IUnitOfWork unitOfWork)
        {
            _repo = repo;
            _appointmentRepo = appointmentRepo;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> HandlerAsync(CreatePrescriptionCommand command, CancellationToken cancellationToken = default)
        {
            var appointment = await _appointmentRepo.GetByIdAsync(command.AppointmentId);

            if (appointment is null)
                return Result.Failure("Appointment not found.");

            var prescription = EM.Hospital.Domain.Entities.Prescription.Create(appointment, command.Medications, command.Instructions);
            await _repo.AddAsync(prescription.Value!);
            await _unitOfWork.SaveChangeAsync();
            return Result<Guid>.Success(prescription.Value!.Id);
        }
    }
}
