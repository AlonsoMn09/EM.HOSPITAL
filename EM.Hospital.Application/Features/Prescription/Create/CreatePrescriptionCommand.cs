using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.Prescription.Create
{
    public class CreatePrescriptionCommand : ICommand<Result>
    {
        public Guid AppointmentId { get; private set; }        
        public string Medications { get; private set; } = default!;
        public string Instructions { get; private set; } = default!;
    }
}
