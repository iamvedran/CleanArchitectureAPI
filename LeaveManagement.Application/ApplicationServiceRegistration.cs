using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace LeaveManagement.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}