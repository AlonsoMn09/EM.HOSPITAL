using EM.Hospital.Application.Common.Contracts.CQRS;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Common.Services.CQRS
{
    public class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider _provider;

        public Dispatcher(IServiceProvider provider)
        {
            _provider = provider;
        }

        public async Task SendAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand
        {
            var handler = _provider.GetRequiredService<ICommandHandler<TCommand>>();
            if (handler == null)
                throw new InvalidOperationException($"No handler registered for command type {typeof(TCommand).Name}");
            await handler.HandlerAsync(command, cancellationToken);
        }

        public async Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken cancellationToken = default) where TCommand : ICommand<TResult>
        {
            var handler = _provider.GetRequiredService<ICommandHandler<TCommand, TResult>>();

            if (handler == null)
                throw new InvalidOperationException($"No handler registered for command type {typeof(TCommand).Name}");

            var behavior = _provider.GetService<IEnumerable<IPipelineBehavior<TCommand, TResult>>>()?.ToArray() ?? Enumerable.Empty<IPipelineBehavior<TCommand, TResult>>();

            RequestHandlerDelaget<TResult> handlerDelegate = () => handler.HandlerAsync(command, cancellationToken);

            var pipeline = behavior.Reverse().Aggregate(handlerDelegate, (next, behavior) =>
            {
                return () => behavior.HandlerAsync(command, cancellationToken, next);
            });

            return await pipeline();
        }

        public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default) where TQuery : IQuery<TResult>
        {
            var handler = _provider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            if (handler == null)
                throw new InvalidOperationException($"No handler registered for query type {typeof(TQuery).Name}");
            return await handler.HandleAsync(query, cancellationToken);
        }
    }
}
