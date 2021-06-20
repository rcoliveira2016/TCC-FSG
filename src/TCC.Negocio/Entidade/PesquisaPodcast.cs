using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Negocio.Entidade
{
    public class PesquisaPodcast
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string NomeEpisodio { get; set; }
        public string ErroTranscricao { get; set; }
    }
}
