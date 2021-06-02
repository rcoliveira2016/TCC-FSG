
using System.Collections.Generic;
using TCC.Negocio.Entidade;
using TCC.Negocio.Interface.Repository;
using TCC.Negocio.Interface.Service;
using System.Linq;

namespace TCC.Negocio.Service
{
    public class PesquisaPodcastService : IPesquisaPodcastService
    {
        private readonly IPesquisaPodcastRepository pesquisaPodcastRepository;
        public PesquisaPodcastService(IPesquisaPodcastRepository pesquisaPodcastRepository)
        {
            this.pesquisaPodcastRepository = pesquisaPodcastRepository;
        }

        public IEnumerable<PesquisaPodcast> Buscar(string termo = null)
        {
            return pesquisaPodcastRepository.Buscar(termo);
        }
    }
}
