using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Domain.Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Common.Services.CQRS
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TResponse : class
    {
        private readonly IEnumerable<IValidator<TRequest>> _validator;

        public ValidationBehavior(IServiceProvider provider)
        {
            _validator = provider.GetService<IEnumerable<IValidator<TRequest>>>() ?? new List<IValidator<TRequest>>();
        }

        public async Task<TResponse> HandlerAsync(TRequest request, CancellationToken cancellationToken, RequestHandlerDelaget<TResponse> next)
        {
            if (_validator.Any())
            {
                var errors = new List<string>();

                foreach (var validator in _validator)
                {
                    var result = await validator.ValidateAsync(request, cancellationToken);
                    if (!result.IsValid)
                    {
                        errors.AddRange(result.Errors.Select(e => $"{e.PropertyName}: {e.Message}"));
                    }
                }

                if (errors.Any())
                {
                    if (typeof(TResponse) == typeof(Result))
                    {
                        return (TResponse)(object)Result.Failure(errors);
                    }

                    if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
                    {
                        var genericArg = typeof(TResponse).GetGenericArguments()[0];

                        var failureMethod = typeof(Result)
                            .GetMethods()
                            .First(x =>
                                x.Name == nameof(Result.Failure) &&
                                x.IsGenericMethodDefinition &&
                                x.GetParameters().Length == 1 &&
                                x.GetParameters()[0].ParameterType == typeof(IEnumerable<string>)
                            );

                        var genericFailure = failureMethod.MakeGenericMethod(genericArg);

                        var result = genericFailure.Invoke(null, new object[]
                        {
                            errors
                        });

                        return (TResponse)(object)result!;
                    }
                }
            }

            return await next();
        }
    }
}
