using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCC.Negocio.Interface.Repository
{
    public interface ICatalogosPodcastsDapperRepository
    {
        void UpdateTrancricao(long id, string trancricao);
        void UpdateErroTrancricao(long id, string erroTrancricao);
    }
}
