using NSonic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TCC.Negocio.Entidade;
using TCC.Negocio.Interface.Service;

namespace TCC.Negocio.Service.Sonic
{
    public class SonicService: ISonicService
    {
        const string hostname = "localhost";
        const int port = 1491;
        const string secret = "SecretPassword";
        public void CadastrarRegistro(CatalogoPodcast catalogoPodcast)
        {
            using (var ingest = NSonicFactory.Ingest(hostname, port, secret))
            {
                ingest.Connect();

                ingest.Push(
                    "Catalogo",
                    "Catalogo",
                    $"Catalogo:{catalogoPodcast.Id}",
                    $"{catalogoPodcast.Nome} {catalogoPodcast.NomeEpisodio} {catalogoPodcast.Transcricao}", 
                    "por"
                   );
            }
        }

        public IEnumerable<CatalogoPodcast> Consultar(string termo)
        {
            if(termo == null) return new List<CatalogoPodcast>();
            var retorno = new List<CatalogoPodcast>();
            using (var search = NSonicFactory.Search(hostname, port, secret))
            {
                search.Connect();

                var queryResults = search.Query("Catalogo", "Catalogo", termo, locale:"por");
                foreach (var item in queryResults)
                {
                    if (item.Contains(":"))
                    {
                        retorno.Add(new CatalogoPodcast()
                        {
                            Id = Convert.ToInt64(item.Split(":").Last())
                        });
                    }
                }
            }

            return retorno;
        }

    }
}
