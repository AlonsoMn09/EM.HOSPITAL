using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Common.Contracts.CQRS
{
    public delegate Task<TResult> RequestHandlerDelaget<TResult>();
    public interface IPipelineBehavior<TRequest, TResponse>
    {
        Task<TResponse> HandlerAsync(TRequest request, CancellationToken cancellationToken, RequestHandlerDelaget<TResponse> next);
    }
}
