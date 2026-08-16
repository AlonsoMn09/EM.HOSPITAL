using EM.Hospital.Application.Common.Contracts.Services.Persistence;
using EM.Hospital.Domain.Common;
using EM.Hospital.Infraestructure.Configuration.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Infraestructure.Services
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly HospitalDbContext _context;
        private readonly DomainEventDispatcher _dispatcher;

        public UnitOfWork(HospitalDbContext context, DomainEventDispatcher dispatcher)
        {
            _context = context;
            _dispatcher = dispatcher;
        }

        public async Task<Result<int>> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            var eventResult = await _dispatcher.DispatcherEventAsync(_context);

            if (eventResult.IsFailure)
            {
                return Result.Failure<int>(eventResult.Errors!);
            }

            var changes = await _context.SaveChangesAsync(cancellationToken);
            return Result.Success(changes);
        }
    }
}
