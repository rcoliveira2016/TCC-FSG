using System.Collections.Generic;
using TCC.Negocio.Entidade;

namespace TCC.Negocio.Interface.Repository
{
    public interface IPesquisaPodcastRepository
    {
        IEnumerable<PesquisaPodcast> Buscar(string termo = null);
    }
}
