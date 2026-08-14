using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Domain.ValueObjects
{
    public class DateTimeRange
    {
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public TimeSpan Duration => End - Start;
        public DateTimeRange()
        {
                
        }
        public DateTimeRange(DateTime start, DateTime end)
        {           
            Start = start;
            End = end;
        }
        public static Result<DateTimeRange> Create(DateTime start, DateTime end)
        {
            if (end < start)
                return Result.Failure<DateTimeRange>("End date must be greater than or equal to start date.");
            return Result.Success(new DateTimeRange(start, end));
        }
    }
}
