using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EM.Hospital.Application.Features.Patient.Create
{
    public class CreatePatientCommand : ICommand<Result<Guid>>
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string DocumentType { get; set; } = default!;
        public string DocumentNumber { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
