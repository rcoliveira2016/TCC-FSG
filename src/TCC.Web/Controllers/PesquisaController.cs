using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Infra.IoC.Helper;
using TCC.Negocio.Entidade;
using TCC.Negocio.Interface.Service;
using TCC.Web.Models.ViewModel;
using TCC.Web.Models.ViewModel.Pesquisa;

namespace TCC.Web.Controllers
{
    public class PesquisaController : Controller
    {
        private readonly IPesquisaPodcastService pesquisaPodcastService;
        public PesquisaController(IPesquisaPodcastService pesquisaPodcastService)
        {
            this.pesquisaPodcastService = pesquisaPodcastService;
        }
        public IActionResult Index()
        {
            return View(pesquisaPodcastService.Buscar().Select(x=> new PesquisaPodcastsViewModel { 
                Id = x.Id,
                Nome = x.Nome,
                NomeEpisodio = x.NomeEpisodio
            }));
        }
    }
}
