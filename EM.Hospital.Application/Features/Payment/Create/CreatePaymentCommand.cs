using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Domain.Common;
using EM.Hospital.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Features.Payment.Create
{
    public class CreatePaymentCommand : ICommand<Result<Guid>>
    {
        public Guid AppointmentId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = string.Empty;

    }
}
