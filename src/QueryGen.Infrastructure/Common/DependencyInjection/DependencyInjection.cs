using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QueryGen.Application.Common.Auth;
using QueryGen.Application.Common.Repository;
using QueryGen.Application.Common.Services;
using QueryGen.Infrastructure.Common.Context;
using QueryGen.Infrastructure.Common.Services;
using QueryGen.Infrastructure.Common.Auth;
using QueryGen.Infrastructure.Session;
using QueryGen.Infrastructure.User;

namespace QueryGen.Infrastructure.Common.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDI
        (this IServiceCollection services , IConfiguration configuration)
    {

        //db
        services.AddDbContext<QueryGenDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("QueryGen"));
        });

        services.AddScoped<QueryGenDbContext>();

        //services
        services.AddScoped<IUserServices, UserServices>();
        services.AddScoped<ISessionServices, SessionServices>();
        services.AddScoped<ILlmServices, OpenRouterLlmServices>();
        services.AddScoped<IDbServices, DbServices>();
        services.AddScoped<IPromptBuilder, QueryPromptBulider>();

        //auth
        services.AddScoped<IAuthServices, JwtAuthServices>();
        services.AddScoped<IPasswordServices, PasswordServices>();

        //repository
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISessionRepository, SessionRepository>();


        return services;
    }
}
