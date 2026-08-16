using EM.Hospital.API.Middlewares;
using EM.Hospital.Application;
using EM.Hospital.Infraestructure;
using EM.Hospital.Infraestructure.Configuration.Authentication.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services
    .AddApplication()
    .AddInfraestructure(builder.Configuration)
    .AddAuthenticationConfig(builder.Configuration);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();