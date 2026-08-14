using EM.Hospital.Domain.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Domain.Events
{
    public class CreatePatientDomainEvent : IDomainEvent
    {
        public Guid PatientId { get; }
        public string FullName { get; }
        public string UserName { get; }
        public string Role { get; }
        public string PasswordHash { get; }
        public string Email { get; }
        public Guid Id => Guid.NewGuid();
        public DateTime OccurredOn => DateTime.UtcNow;
        public CreatePatientDomainEvent(Guid patientId, string fullName, string userName, string role, string passwordHash, string email)
        {
            PatientId = patientId;
            FullName = fullName;
            UserName = userName;
            Role = role;
            PasswordHash = passwordHash;
            Email = email;
        }
    }
}
