using EM.Hospital.Domain.Common;
using EM.Hospital.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public Guid PatientId { get; private set; }
        public Guid DoctorId { get; private set; }
        public DateTime ScheduledAt { get; private set; }
        public AppointmentStatus Status { get; private set; }
        public Payment? Payment { get; private set; }
        public Prescription? Prescription { get; private set; }
        public Appointment()
        {
                
        }
        private Appointment(Guid patientId, Guid doctorId, DateTime scheduledAt)
        {
            PatientId = patientId;
            DoctorId = doctorId;
            ScheduledAt = scheduledAt;
            Status = AppointmentStatus.Scheduled;
        }
        public static Result<Appointment> Create(Guid patientId, Guid doctorId, DateTime scheduledAt)
        {
            if (scheduledAt <= DateTime.UtcNow)
                return Result.Failure<Appointment>("Cannot schedule an appointment in the past.");

            return Result.Success(new Appointment(patientId, doctorId, scheduledAt));
        }
    }
}
