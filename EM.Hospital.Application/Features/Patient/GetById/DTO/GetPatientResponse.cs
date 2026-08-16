using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.Patient.GetById.DTO
{
    public class GetPatientResponse
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string DocumentType { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
    }
}
