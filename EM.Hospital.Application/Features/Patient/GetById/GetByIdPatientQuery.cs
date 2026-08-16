using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Features.Patient.GetById.DTO;
using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.Patient.GetById
{
    public class GetByIdPatientQuery : IQuery<Result<GetPatientResponse>>
    {
        public Guid Id { get; set; }
    }
}
