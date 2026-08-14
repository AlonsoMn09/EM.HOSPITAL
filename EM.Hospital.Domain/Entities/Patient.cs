using EM.Hospital.Domain.Common;
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
        public Patient()
        {
            
        }
        private Patient(string firstName, string lastName, string email, string phone)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }
        public static Result<Patient> Create(string firstName, string lastName, string email, string phone)
        {
            if (string.IsNullOrEmpty(firstName))
                return Result.Failure<Patient>("First name is required");
            if (string.IsNullOrEmpty(lastName))
                return Result.Failure<Patient>("Last name is required");
            if (string.IsNullOrEmpty(email))
                return Result.Failure<Patient>("Email is required");
            if (string.IsNullOrEmpty(phone))
                return Result.Failure<Patient>("Phone is required");
            return Result.Success(new Patient(firstName, lastName, email, phone));
        }
    }
}
