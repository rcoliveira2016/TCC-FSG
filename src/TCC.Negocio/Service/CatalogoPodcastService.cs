
using TCC.Negocio.Interface.Repository;
using TCC.Negocio.Interface.Service;

namespace TCC.Negocio.Service
{
    public class CatalogoPodcastService: ICatalogoPodcastService
    {
        private readonly ICatalogoPodcastRepository catalogoPodcastRepository;
        public CatalogoPodcastService(ICatalogoPodcastRepository catalogoPodcastRepository)
        {
            this.catalogoPodcastRepository = catalogoPodcastRepository;
        }
    }
}
