using BuildingBlocks.Domain.Notifications;
using BuildingBlocks.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using TCC.Infra.IoC.Helper;
using TCC.Negocio.Interface.Service;
using TCC.Web.Models.ViewModel.Catalogo;

namespace TCC.Web.Controllers.Api
{
    public class ApiCatalogoController : ApiController
    {
        private readonly ICatalogoPodcastService catalogoPodcastService;
        public ApiCatalogoController(
            INotificationHandler<DomainNotification> notifications, 
            ICatalogoPodcastService catalogoPodcastService) : base(notifications)
        {
            this.catalogoPodcastService = catalogoPodcastService;
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody]CadastroCatalogosPodcastsViewModel catalogosPodcastsViewModel)
        {

            var idCadastrado = catalogoPodcastService.Cadastrar(new()
            {
                Audio = Convert.FromBase64String(catalogosPodcastsViewModel.Audio),
                Nome = catalogosPodcastsViewModel.Nome,
                NomeEpisodio = catalogosPodcastsViewModel.NomeEpisodio,
                UrlSitePodcast = catalogosPodcastsViewModel.UrlSitePodcast
            });

            return Response(idCadastrado);
        }

    }
}
