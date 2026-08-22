
using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Features.Patient.Create;
using System;

namespace EM.Hospital.Application.Features.Appointment.Create
{
    public class CreateAppointmentCommandValidator : IValidator<CreateAppointmentCommand>
    {
        public Task<ValidationResult> ValidateAsync(CreateAppointmentCommand instance, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}


