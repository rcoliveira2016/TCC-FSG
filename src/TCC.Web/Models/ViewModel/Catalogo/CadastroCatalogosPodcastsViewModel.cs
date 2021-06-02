using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace TCC.Web.Models.ViewModel.Catalogo
{
    public class CadastroCatalogosPodcastsViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Nome é obrigatorio")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Nome do Episódio é obrigatorio")]
        [Display(Name = "Nome do Episódio")]
        public string NomeEpisodio { get; set; }
        [Display(Name = "Url do Site Podcast")]
        public string UrlSitePodcast { get; set; }
        [Display(Name = "Transcrição")]
        public string Transcricao { get; set; }
        [Required(ErrorMessage = "Audio podcast é obrigatorio")]
        [Display(Name = "Audio podcast")]
        public string Audio { get; set; }
    }
}
