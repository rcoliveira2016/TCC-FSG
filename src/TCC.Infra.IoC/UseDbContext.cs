using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using TCC.Infra.Context;

namespace TCC.Infra.IoC
{
    public static class UseDbContext
    {
        public static void RegisterBdContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TccContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("TccContext"))
            );
        }

        public static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<TccContext>();
                    context.Database.EnsureCreated();
                }
                catch (Exception ex)
                {
                    
                }
            }
        }

    }
}
