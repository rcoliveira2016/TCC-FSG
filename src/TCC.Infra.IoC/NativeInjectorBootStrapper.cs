using BuildingBlocks.Domain.Notifications;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TCC.Infra.Data.Dapper;
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
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddScoped<ICatalogoPodcastService, CatalogoPodcastService>();
            services.AddScoped<ITranscreverPodcastService, TranscreverPodcastService>();
            services.AddScoped<IPesquisaPodcastService, PesquisaPodcastService>();


            services.AddScoped<ICatalogoPodcastRepository, CatalogosPodcastsRepository>();
            services.AddScoped<IPesquisaPodcastRepository, PesquisaPodcastRepository>();
            services.AddScoped<ICatalogosPodcastsDapperRepository, CatalogosPodcastsDapperRepository>();

            services.BuildServiceProvider();
        }
    }
}
