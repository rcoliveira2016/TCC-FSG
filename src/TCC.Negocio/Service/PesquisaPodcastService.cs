
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
        private readonly ISonicService sonicService;
        public PesquisaPodcastService(IPesquisaPodcastRepository pesquisaPodcastRepository, ISonicService sonicService)
        {
            this.pesquisaPodcastRepository = pesquisaPodcastRepository;
            this.sonicService = sonicService;
        }

        public IEnumerable<PesquisaPodcast> Buscar(string termo = null)
        {
            if (string.IsNullOrEmpty(termo))
                return pesquisaPodcastRepository.Buscar();

            var resultado = sonicService.Consultar(termo);
            return pesquisaPodcastRepository.Buscar(resultado.Select(x=> x.Id).ToArray());
        }

    }
}
