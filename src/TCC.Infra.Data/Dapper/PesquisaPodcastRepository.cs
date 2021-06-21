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
        public IEnumerable<PesquisaPodcast> Buscar(long[] ids = null)
        {
            if(!ids?.Any() ?? false)
                return Enumerable.Empty<PesquisaPodcast>();

            return ExecuteQuery<PesquisaPodcast>(@$"
                {SqlBase()} 
                where 
                    id in @ids", new { ids });
        }
        public IEnumerable<PesquisaPodcast> Buscar()
        {
            return ExecuteQuery<PesquisaPodcast>(SqlBase());
        }
        private static string SqlBase()
        {
            return @"SELECT 
                    Id,
                    Nome,
                    NomeEpisodio,
                    ErroTranscricao
                from
                    CatalogosPodcasts";
        }
    }
}
