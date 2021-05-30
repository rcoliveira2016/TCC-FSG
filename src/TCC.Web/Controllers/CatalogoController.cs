using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCC.Web.Models.ViewModel;

namespace TCC.Web.Controllers
{
    public class CatalogoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View(new CatalogosPodcastsViewModel { });
        }
        [HttpPost]
        public IActionResult Cadastrar(CatalogosPodcastsViewModel catalogosPodcastsViewModel)
        {
            if(!ModelState.IsValid)
                return View(catalogosPodcastsViewModel);

            return View(catalogosPodcastsViewModel);
        }
    }
}
