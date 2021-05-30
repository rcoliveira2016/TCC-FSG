using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TCC.Infra.Data.EntityFramework;
using TCC.Negocio.Interface.Repository;
using TCC.Negocio.Interface.Service;
using TCC.Negocio.Service;

namespace TCC.Infra.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(this IServiceCollection services, in IConfiguration configuration)
        {

            services.AddScoped<ICatalogoPodcastService, CatalogoPodcastService>();
            services.AddScoped<ICatalogoPodcastRepository, CatalogosPodcastsRepository>();

            services.BuildServiceProvider();
        }
    }
}
