using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Common.Models.Auth
{
    public class User
    {
        public string UserName { get; private set; }
        public string Role { get; private set; }
        public string PasswordHash { get; private set; }
        public string PatientFullName { get; private set; } //CustomerFullName
        public string Email { get; private set; }
        public Guid PatientId { get; private set; }
        public string Id { get; private set; }

        private User(string userName, string passwordHash, string fullName, string email, Guid customerId, string id, string role)
        {
            UserName = userName;
            PasswordHash = passwordHash;
            PatientFullName = fullName;
            Email = email;
            PatientId = customerId;
            Id = id;
            Role = role;
        }

        public static Result<User> Create(string userName, string passwordHash, string fullName, string email, Guid customerId, string id, string role)
        {
            return Result.Success(new User(userName, passwordHash, fullName, email, customerId, id, role));
        }
    }
}
