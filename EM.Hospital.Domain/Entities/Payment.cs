using EM.Hospital.Domain.Common;
using EM.Hospital.Domain.Enums;
using EM.Hospital.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid AppointmentId { get; set; }
        public Money Amount { get; set; }
        public DateTime Date { get; set; }
        public PaymentMethod Method { get; set; }
        public PaymentStatus Status { get; set; }
        public Payment()
        {
            
        }
        private Payment(Guid appointmentId, Money amount, DateTime date, PaymentMethod method, PaymentStatus status)
        {
            AppointmentId = appointmentId;
            Amount = amount;
            Date = date;
            Method = method;
            Status = status;
        }
        public static Result<Payment> Create(Guid appointmentId, Money amount, DateTime date, PaymentMethod method, PaymentStatus status)
        {
            if (appointmentId == Guid.Empty)
                return Result.Failure<Payment>("Appointment ID is required");
            if (amount == null)
                return Result.Failure<Payment>("Amount is required");
            if (date == default)
                return Result.Failure<Payment>("Date is required");
            if (!Enum.IsDefined(typeof(PaymentMethod), method))
                return Result.Failure<Payment>("Invalid payment method");
            if (!Enum.IsDefined(typeof(PaymentStatus), status))
                return Result.Failure<Payment>("Invalid payment status");
            return Result.Success(new Payment(appointmentId, amount, date, method, status));
        }
    }
}
