using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Domain.Events.Interfaces
{
    public interface IDomainEvent
    {
        Guid Id { get; }
        DateTime OccurredOn { get; }
    }
}
