using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Domain.ValueObjects
{
    public class Money
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }
        public Money()
        {
            
        }
        private Money(decimal amount, string currency)
        {            
            Amount = amount;
            Currency = currency;
        }
        public static Result<Money> Create(decimal amount, string currency)
        {
            if (amount < 0)
                return Result.Failure<Money>("Amount cannot be negative");
            if (string.IsNullOrEmpty(currency))
                return Result.Failure<Money>("Currency is required");
            return Result.Success(new Money(amount, currency));
        }
        public static Money operator +(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new InvalidOperationException("Cannot add money with different currencies");
            return new Money(a.Amount + b.Amount, a.Currency);
        }
    }
}
