using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Common.Contracts.Repositories;
using EM.Hospital.Application.Common.Contracts.Services.Persistence;
using EM.Hospital.Domain.Common;
using EM.Hospital.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EM.Hospital.Application.Features.Appointment.Create
{
    public class CreateAppointmentCommandHandler : ICommandHandler<CreateAppointmentCommand, Result<Guid>>
    {
        private readonly IAppointmentRepository _appointmentRepo;
        private readonly IPatientRepository _patientRepo;
        private readonly IDoctorRepository _doctorRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAppointmentCommandHandler(
            IAppointmentRepository appointmentRepo,
            IPatientRepository patientRepo,
            IDoctorRepository doctorRepo,
            IUnitOfWork unitOfWork)
        {
            _appointmentRepo = appointmentRepo;
            _patientRepo = patientRepo;
            _doctorRepo = doctorRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> HandlerAsync(CreateAppointmentCommand command, CancellationToken cancellationToken = default)
        {
            var patient = await _patientRepo.GetByIdAsync(command.PatientId);
            if (patient == null)
                return Result.Failure<Guid>("Patient not found.");

            var doctor = await _doctorRepo.GetByIdAsync(command.DoctorId);
            if (doctor == null)
                return Result.Failure<Guid>("Doctor not found.");

            var existing = await _appointmentRepo.FindByPredicateAsync(a => a.DoctorId == command.DoctorId && a.ScheduledAt == command.ScheduledAt && a.Active);
            if (existing != null)
                return Result.Failure<Guid>("The doctor already has an appointment scheduled at the specified time.");

            var appointmentResult = EM.Hospital.Domain.Entities.Appointment.Create(patient, doctor, command.ScheduledAt);
            if (appointmentResult.IsFailure)
                return Result.Failure<Guid>(appointmentResult.Errors);

            await _appointmentRepo.AddAsync(appointmentResult.Value!);
            await _unitOfWork.SaveChangeAsync();

            return Result<Guid>.Success(appointmentResult.Value!.Id);
        }
    }
}
