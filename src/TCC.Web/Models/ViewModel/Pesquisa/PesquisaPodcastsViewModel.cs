using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.Web.Models.ViewModel.Pesquisa
{
    public class PesquisaPodcastsViewModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string NomeEpisodio { get; set; }
    }
}
