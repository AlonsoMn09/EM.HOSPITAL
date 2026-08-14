using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public Guid SpecialityId { get; private set; }
        private readonly List<DoctorSchedule> _doctorSchedules = new List<DoctorSchedule>();
        public IReadOnlyCollection<DoctorSchedule> DoctorSchedules => _doctorSchedules.AsReadOnly();
        public Doctor()
        {

        }
        private Doctor(string firstName, string lastName, string email, string phone, Guid specialityId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            SpecialityId = specialityId;
        }
        public static Result<Doctor> Create(string firstName, string lastName, string email, string phone, Guid specialityId)
        {
            if (string.IsNullOrEmpty(firstName))
                return Result.Failure<Doctor>("First name is required");
            if (string.IsNullOrEmpty(lastName))
                return Result.Failure<Doctor>("Last name is required");
            if (string.IsNullOrEmpty(email))
                return Result.Failure<Doctor>("Email is required");
            if (string.IsNullOrEmpty(phone))
                return Result.Failure<Doctor>("Phone is required");
            if (specialityId == Guid.Empty)
                return Result.Failure<Doctor>("Speciality is required");
            return Result.Success(new Doctor(firstName, lastName, email, phone, specialityId));
        }
        public void AddSchedule(DoctorSchedule schedule)
        {
            _doctorSchedules.Add(schedule);
        }
    }
}
