using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EM.Hospital.Application.Features.Patient.Delete
{
    public class DeletePatientCommand : ICommand<Result>
    {
        public Guid Id { get; set; }
    }
}
