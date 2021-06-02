
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
        public CatalogoPodcastService(ICatalogoPodcastRepository catalogoPodcastRepository, ITranscreverPodcastService transcreverPodcastService)
        {
            this.catalogoPodcastRepository = catalogoPodcastRepository;
            this.transcreverPodcastService = transcreverPodcastService;
        }

        public long? Cadastrar(CatalogoPodcast catalogoPodcast)
        {
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
