using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Domain.Enums
{
    public enum PaymentStatus
    {
        Pending = 1,
        Completed = 2,
        Refunded = 3
    }
    public enum PaymentMethod
    {
        Cash = 1,
        CreditCard = 2,
        DebitCard = 3,
        BankTransfer = 4
    }
}
