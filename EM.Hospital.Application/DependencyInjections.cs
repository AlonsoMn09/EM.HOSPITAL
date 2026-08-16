using EM.Hospital.Application.Common.Contracts.CQRS;
using EM.Hospital.Application.Common.Services.CQRS;
using EM.Hospital.Application.Features.Users.Create;
using EM.Hospital.Domain.Events;
using EM.Hospital.Domain.Events.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EM.Hospital.Application
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IDispatcher, Dispatcher>();

            services.Scan(scan => scan
                .FromAssemblies(typeof(IDispatcher).Assembly)

                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()

                .AddClasses(classes => classes.AssignableTo(typeof(IPipelineBehavior<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
            
            services.AddScoped<IDomainEventHandler<CreatePatientDomainEvent>, CreateUserEventHandler>();
            return services;
        }
    }
}
