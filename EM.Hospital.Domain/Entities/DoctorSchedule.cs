using EM.Hospital.Domain.Common;
using EM.Hospital.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Domain.Entities
{
    public class DoctorSchedule : BaseEntity
    {        
        public DayOfWeek DayOfWeek { get; private set; }
        public DateTimeRange Schedule { get; private set; }
        public DoctorSchedule()
        {
            
        }
        public DoctorSchedule(DayOfWeek dayOfWeek, DateTimeRange schedule)
        {            
            DayOfWeek = dayOfWeek;
            Schedule = schedule;
        }
        public static Result<DoctorSchedule> Create(DayOfWeek dayOfWeek, DateTimeRange schedule)
        {
            if (schedule == null)
                return Result.Failure<DoctorSchedule>("Schedule is required");
            return Result.Success(new DoctorSchedule(dayOfWeek, schedule));
        }
    }
}
