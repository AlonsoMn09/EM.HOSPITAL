using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Domain.Common;
using System;

namespace EM.Hospital.Application.Features.Appointment.Create
{
    public class CreateAppointmentCommand : ICommand<Result<Guid>>
    {
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime ScheduledAt { get; set; }
    }
}
