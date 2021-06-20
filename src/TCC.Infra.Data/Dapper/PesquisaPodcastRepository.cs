using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCC.Infra.Data.Dapper.common;
using TCC.Negocio.Entidade;
using TCC.Negocio.Interface.Repository;

namespace TCC.Infra.Data.Dapper
{
    public sealed class PesquisaPodcastRepository : DapperBaseRepository, IPesquisaPodcastRepository
    {
        public PesquisaPodcastRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public IEnumerable<PesquisaPodcast> Buscar(string termo = null)
        {
            termo = string.IsNullOrEmpty(termo) ? null : $"%{termo.ToUpper()}%";
            return ExecuteQuery<PesquisaPodcast>(@"
                SELECT 
                    Id,
                    Nome,
                    NomeEpisodio,
                    ErroTranscricao
                from 
                    CatalogosPodcasts 
                where 
                    @termo IS NULL or (Upper(Nome) like @termo OR 
                    Upper(NomeEpisodio) like @termo OR
                    Upper(Transcricao) like @termo)", new { termo });
        }
    }
}
