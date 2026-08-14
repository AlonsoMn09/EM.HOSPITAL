using EM.Hospital.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Common.Contracts.Services.Persistence
{
    public interface IUnitOfWork
    {
        Task<Result<int>> SaveChangeAsync(CancellationToken cancellationToken = default);
    }
}
