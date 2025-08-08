using System;
using Microsoft.Extensions.DependencyInjection;

namespace QueryGen.Application.Common.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationDI(this IServiceCollection services)
    {
        services.AddMediatR(options =>
            options.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));


        return services;        
    }
}
