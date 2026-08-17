using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Domain.Common;
using EM.Hospital.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.DoctorSchedule.Create
{
    public class CreateDoctorScheduleCommand : ICommand<Result<Guid>>
    {
        public DayOfWeek DayOfWeek { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
