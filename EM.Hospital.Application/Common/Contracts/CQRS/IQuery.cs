using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Common.Contracts.CQRS
{
    public interface IQuery<TResult> { }

    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}
