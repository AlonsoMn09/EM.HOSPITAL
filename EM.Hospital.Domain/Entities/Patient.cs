using EM.Hospital.Domain.Common;
using EM.Hospital.Domain.Events;
using EM.Hospital.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Domain.Entities
{
    public class Patient : BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public IdentityDocument Document { get; private set; }
        private readonly List<Appointment> _appointments = new List<Appointment>();
        public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly(); 
        public Patient()
        {
            
        }
        private Patient(string firstName, string lastName, string email, string phone, IdentityDocument document, string user, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Document = document;

            AddDomainEvent(new CreatePatientDomainEvent(Id, $"{FirstName} {LastName}", user, "Patient", password, Email));
        }
        public static Result<Patient> Create(string firstName, string lastName, string email, string phone, IdentityDocument document, string user, string password)
        {
            if (string.IsNullOrEmpty(firstName))
                return Result.Failure<Patient>("First name is required");
            if (string.IsNullOrEmpty(lastName))
                return Result.Failure<Patient>("Last name is required");
            if (string.IsNullOrEmpty(email))
                return Result.Failure<Patient>("Email is required");
            if (string.IsNullOrEmpty(phone))
                return Result.Failure<Patient>("Phone is required");
            return Result.Success(new Patient(firstName, lastName, email, phone, document, user, password));
        }
        public static Result<Patient> Update(Patient patient, string firstName, string lastName, string email, string phone, IdentityDocument document)
        {
            if (patient == null)
                return Result.Failure<Patient>("Patient is required");
            if (string.IsNullOrEmpty(firstName))
                return Result.Failure<Patient>("First name is required");
            if (string.IsNullOrEmpty(lastName))
                return Result.Failure<Patient>("Last name is required");
            if (string.IsNullOrEmpty(email))
                return Result.Failure<Patient>("Email is required");
            if (string.IsNullOrEmpty(phone))
                return Result.Failure<Patient>("Phone is required");
            patient.FirstName = firstName;
            patient.LastName = lastName;
            patient.Email = email;
            patient.Phone = phone;
            patient.Document = document;
            patient.UpdatedAt = DateTime.UtcNow;
            patient.UpdatedBy = Environment.UserName;
            return Result.Success(patient);
        }
    }
}
