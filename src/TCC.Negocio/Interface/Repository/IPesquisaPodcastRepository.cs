using System.Collections.Generic;
using TCC.Negocio.Entidade;

namespace TCC.Negocio.Interface.Repository
{
    public interface IPesquisaPodcastRepository
    {
        IEnumerable<PesquisaPodcast> Buscar(long[] ids);
        IEnumerable<PesquisaPodcast> Buscar();
    }
}
