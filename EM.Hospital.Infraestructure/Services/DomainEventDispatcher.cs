using EM.Hospital.Domain.Common;
using EM.Hospital.Domain.Events.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EM.Hospital.Infraestructure.Services
{
    public class DomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        public DomainEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<Result> DispatcherEventAsync(DbContext context)
        {
            var entities = context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(p => p.Entity.DomainEvents.Any())
                .ToList();

            var events = entities.SelectMany(p => p.Entity.DomainEvents).ToList();

            entities.ForEach(e => e.Entity.RemoveDomainEvent());

            foreach (var e in events)
            {
                var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(e.GetType());
                var handlers = _serviceProvider.GetServices(handlerType);
                foreach (var handler in handlers)
                {
                    var method = handlerType.GetMethod("HandlerAsync");
                    var result = await (Task<Result>)method!.Invoke(handler, new object[] { e })!;

                    if (result.IsFailure)
                    {
                        return result;
                    }
                }
            }

            return Result.Success();
        }
    }
}
