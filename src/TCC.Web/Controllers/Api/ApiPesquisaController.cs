using BuildingBlocks.Domain.Notifications;
using BuildingBlocks.WebApi.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TCC.Negocio.Interface.Service;
using TCC.Web.Models.ViewModel.Pesquisa;

namespace TCC.Web.Controllers.Api
{
    public class ApiPesquisaController : ApiController
    {
        private readonly IPesquisaPodcastService pesquisaPodcastService;
        public ApiPesquisaController(IPesquisaPodcastService pesquisaPodcastService, INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            this.pesquisaPodcastService = pesquisaPodcastService;
        }

        public IActionResult Buscar([FromBody] PesquisaPodcastsInputModel inputModel)
        {
            return Response(pesquisaPodcastService.Buscar(inputModel?.termo).Select(x => new PesquisaPodcastsViewModel
            {
                Id = x.Id,
                Nome = x.Nome,
                NomeEpisodio = x.NomeEpisodio
            }));
        }
    }
}
