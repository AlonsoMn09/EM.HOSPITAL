using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EM.Hospital.Application.Features.Specialty.Create
{
    public class CreateSpecialityCommand : ICommand<Result<Guid>>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
