using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Domain.Entities
{
    public class Prescription : BaseEntity
    {
        public Guid AppointmentId { get; private set; }
        public Appointment appointment { get; set; }
        public string Medications { get; private set; } = default!;
        public string Instructions { get; private set; } = default!;
        public DateTime IssuedAt { get; private set; } = DateTime.UtcNow;
        public Prescription()
        {
            
        }
        private Prescription(Guid appointmentId, string medications, string instructions)
        {
            AppointmentId = appointmentId;
            Medications = medications;
            Instructions = instructions;
        }
        public static Result<Prescription> Create(Appointment appointment, string medications, string instructions)
        {
            if (appointment == null)
                return Result.Failure<Prescription>("Appointment is required");
            if (string.IsNullOrEmpty(medications))
                return Result.Failure<Prescription>("Medications are required");
            if (string.IsNullOrEmpty(instructions))
                return Result.Failure<Prescription>("Instructions are required");
            return Result.Success(new Prescription(appointment.Id, medications, instructions));
        }
    }
}
