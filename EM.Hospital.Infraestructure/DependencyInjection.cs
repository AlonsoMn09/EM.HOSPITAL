using EM.Hospital.Infraestructure.Adapters.Repositories;
using EM.Hospital.Infraestructure.Adapters.Services.Auth;
using EM.Hospital.Infraestructure.Configuration.Authentication.Context;
using EM.Hospital.Infraestructure.Configuration.Persistance.Context;
using EM.Hospital.Infraestructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EM.Hospital.Infraestructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<HospitalDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DbHospital"));
            });

            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DbHospital"));
            });

            services.Scan(p => p
                .FromAssembliesOf(typeof(PatientRepository), typeof(UnitOfWork), typeof(AuthService))
                .AddClasses(cls => cls.Where(p => p.Name.EndsWith("Repository") || p.Name.EndsWith("UnitOfWork") || p.Name.EndsWith("Service")))
                .UsingRegistrationStrategy(Scrutor.RegistrationStrategy.Skip)
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddScoped<DomainEventDispatcher>();
            return services;
        }
    }
}
