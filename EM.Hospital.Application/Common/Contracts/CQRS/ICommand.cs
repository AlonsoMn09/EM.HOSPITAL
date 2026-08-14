using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Application.Common.Contracts.CQRS
{
    public interface ICommand { }

    public interface ICommandHandler<TCommand> where TCommand : ICommand
    {
        Task HandlerAsync(TCommand command, CancellationToken cancellationToken = default);
    }

    public interface ICommand<TResult> : ICommand { }

    public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        Task<TResult> HandlerAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}
