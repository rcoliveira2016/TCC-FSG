using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Infra.IoC.Helper;
using TCC.Negocio.Entidade;
using TCC.Negocio.Interface.Service;
using TCC.Web.Models.ViewModel;
using TCC.Web.Models.ViewModel.Catalogo;

namespace TCC.Web.Controllers
{
    [Controller]
    public class CatalogoController : Controller
    {
        private readonly ICatalogoPodcastService catalogoPodcastService;
        public CatalogoController(ICatalogoPodcastService catalogoPodcastService)
        {
            this.catalogoPodcastService = catalogoPodcastService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View(new CadastroCatalogosPodcastsViewModel { });
        }

        public IActionResult Vizualizar(long id, string termo = null)
        {
            ViewBag.TermoPesquisado = termo;
            var model = catalogoPodcastService.Visualizar(id);
            return View(new VizualizarCatalogosPodcastsViewModel
            {
                Audio = Convert.ToBase64String(model.Audio),
                Id = model.Id,
                ErroTranscricao = model.ErroTranscricao,
                Nome = model.Nome,
                NomeEpisodio = model.NomeEpisodio,
                Transcricao = model.Transcricao,
                UrlSitePodcast = model.UrlSitePodcast,
                DataCadastro = model.DataCadastro
            }) ;
        }
    }
}
