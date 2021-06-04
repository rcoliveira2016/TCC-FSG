
using BuildingBlocks.Domain.Notifications;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using TCC.Negocio.Entidade;
using TCC.Negocio.Interface.Repository;
using TCC.Negocio.Interface.Service;

namespace TCC.Negocio.Service
{
    public class CatalogoPodcastService: ICatalogoPodcastService
    {
        private readonly ICatalogoPodcastRepository catalogoPodcastRepository;
        private readonly ITranscreverPodcastService transcreverPodcastService;
        private readonly IMediator bus;

        public CatalogoPodcastService(ICatalogoPodcastRepository catalogoPodcastRepository, ITranscreverPodcastService transcreverPodcastService, IMediator bus)
        {
            this.catalogoPodcastRepository = catalogoPodcastRepository;
            this.transcreverPodcastService = transcreverPodcastService;
            this.bus = bus;
        }

        public long? Cadastrar(CatalogoPodcast catalogoPodcast)
        {
            if(catalogoPodcastRepository.dbContext.Set<CatalogoPodcast>().Any(x=> x.Nome == catalogoPodcast.Nome && x.NomeEpisodio == catalogoPodcast.NomeEpisodio))
            {
                bus.Publish(DomainNotification.Create("erro", "Nome e Episodeos repetidos", DomainNotificationType.Error));
                return 0;
            }

            catalogoPodcast.DataCadastro = DateTime.Now;
            catalogoPodcastRepository.dbContext.Add(catalogoPodcast);
            catalogoPodcastRepository.dbContext.SaveChanges();

            var newThread = new Thread(() => transcreverPodcastService.Transcrever(catalogoPodcast));
            newThread.Start();

            return catalogoPodcast.Id;
        }

        public CatalogoPodcast Visualizar(long id)
        {
            return catalogoPodcastRepository.dbContext.Set<CatalogoPodcast>().FirstOrDefault(x => x.Id == id);
        }
    }
}
