using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Common.DTO;
using EM.Hospital.Application.Features.Patient.List.DTO;
using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.Patient.List
{
    public class ListPatientQuery : PagedRequest, IQuery<Result<PagedResponse<ListPatientResponse>>>;
}
