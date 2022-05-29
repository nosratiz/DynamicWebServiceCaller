using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebServiceCaller.Application.Common.Interfaces;
using WebServiceCaller.Persistence.Context;

namespace WebServiceCaller.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebServiceNotificationContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("WebServiceContext")));

            services.AddScoped<IWebServiceNotificationContext>(provider => provider.GetService<WebServiceNotificationContext>());

            return services;
        }
    }
}