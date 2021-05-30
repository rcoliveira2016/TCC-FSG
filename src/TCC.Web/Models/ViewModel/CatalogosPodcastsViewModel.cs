using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TCC.Web.Models.ViewModel
{
    public class CatalogosPodcastsViewModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string NomeEpisodio { get; set; }
        public string UrlSitePodcast { get; set; }
        public string Transcricao { get; set; }
        public IFormFile Audio { get; set; }
    }
}
