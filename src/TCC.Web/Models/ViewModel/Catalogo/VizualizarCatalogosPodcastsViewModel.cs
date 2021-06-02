using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace TCC.Web.Models.ViewModel.Catalogo
{
    public class VizualizarCatalogosPodcastsViewModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "Nome é obrigatorio")]
        public string Nome { get; set; }
        [Display(Name = "Nome do Episódio")]
        public string NomeEpisodio { get; set; }
        [Display(Name = "Url do Site Podcast")]
        public string UrlSitePodcast { get; set; }
        [Display(Name = "Transcrição")]
        public string Transcricao { get; set; }
        [Display(Name = "Erro na Transcrição")]
        public string ErroTranscricao { get; set; }
        [Display(Name = "Data Cadastro")]
        public DateTime DataCadastro { get; set; }
        public string Audio { get; set; }

        public bool estaCarregando => string.IsNullOrEmpty(ErroTranscricao) && string.IsNullOrEmpty(Transcricao);
    }
}
