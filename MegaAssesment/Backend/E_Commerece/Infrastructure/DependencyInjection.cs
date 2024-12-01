using App.Core;
using App.Core.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationManager configuration)
        {
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IFileService, FileService>();

            services.AddDbContext<AppDbContext>((provide, options) =>
            {
                options.UseSqlServer((configuration.GetConnectionString("DefaultConnection")));
            });
            return services;
        }

    }
}
