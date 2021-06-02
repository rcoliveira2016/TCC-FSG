using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using TCC.Negocio.Entidade;
using TCC.Negocio.Interface.Repository;
using TCC.Negocio.Interface.Service;

namespace TCC.Negocio.Service
{
    public sealed class TranscreverPodcastService : ITranscreverPodcastService
    {
        private readonly ICatalogosPodcastsDapperRepository catalogosPodcastsDapperRepository;
        private readonly string location = "us-south";
        private readonly string instanceId = "6f6ae3b7-10f8-4cd8-9646-1e6dcd6b5966";
        private readonly string model = "pt-BR_BroadbandModel";
        private readonly string chaveApi = "kTo3Tpdw1Sb5A7-lIgdHlzv9hWuQ_RRQKA9v0KdfEPWz";

        private ArraySegment<byte> openingMessage = new ArraySegment<byte>(Encoding.UTF8.GetBytes(
           "{\"action\": \"start\", \"content-type\": \"audio/mp3\", \"word_confidence \" : true }"
       ));
        private ArraySegment<byte> closingMessage = new ArraySegment<byte>(Encoding.UTF8.GetBytes(
            @"{""action"": ""stop""}"
        ));

        public TranscreverPodcastService(ICatalogosPodcastsDapperRepository catalogosPodcastsDapperRepository)
        {
            this.catalogosPodcastsDapperRepository = catalogosPodcastsDapperRepository;
        }

        public void Transcrever(CatalogoPodcast catalogoPodcast)
        {
            try
            {
                Transcribe(catalogoPodcast);
            }
            catch (Exception exe)
            {
                catalogosPodcastsDapperRepository.UpdateErroTrancricao(catalogoPodcast.Id, exe.ToString());
            }
        }

        private string ObterToken()
        {
            var url = "https://iam.cloud.ibm.com/identity/token";
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "urn:ibm:params:oauth:grant-type:apikey"),
                new KeyValuePair<string, string>("apikey", chaveApi),
            });

            var httpClient = new HttpClient();
            var response = httpClient.PostAsync(url, content).Result;
            var data = response.Content.ReadAsStringAsync().Result;

            return JsonSerializer.Deserialize<TokenIBM>(data).access_token;
        }

        private Uri ObterUrlSpeechToText()
        {
            var urlBase = new StringBuilder($"wss://api.{location}.speech-to-text.watson.cloud.ibm.com/instances/{instanceId}/v1/recognize?");

            (string chave, string valor)[] parametros = new (string, string)[]
            {
                ("access_token", ObterToken()),
                ("model", model)
            };

            parametros.ToList().ForEach(x => {
                urlBase.Append($"{x.chave}={HttpUtility.UrlEncode(x.valor)}&");
            });

            return new Uri(urlBase.ToString().TrimEnd('&'));
        }
        private void Transcribe(CatalogoPodcast catalogoPodcast)
        {
            var ws = new ClientWebSocket();
            var url = ObterUrlSpeechToText();
            ws.ConnectAsync(url, CancellationToken.None).Wait();

            Task.WaitAll(ws.SendAsync(openingMessage, WebSocketMessageType.Text, true, CancellationToken.None), HandleResults(ws, catalogoPodcast));

            Task.WaitAll(SendAudio(ws, catalogoPodcast.Audio), HandleResults(ws, catalogoPodcast));

            ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Close", CancellationToken.None).Wait();
        }


        private async Task SendAudio(ClientWebSocket ws, byte[] arquivoAudio)
        {

            using (var fs = new MemoryStream(arquivoAudio))
            {
                byte[] b = new byte[1024];
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    await ws.SendAsync(new ArraySegment<byte>(b), WebSocketMessageType.Binary, true, CancellationToken.None);
                }
                await ws.SendAsync(closingMessage, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }

        // prints results until the connection closes or a delimeterMessage is recieved
        private async Task HandleResults(ClientWebSocket ws, CatalogoPodcast catalogoPodcast)
        {
            var buffer = new byte[catalogoPodcast.Audio.Length];
            while (true)
            {
                var segment = new ArraySegment<byte>(buffer);

                var result = await ws.ReceiveAsync(segment, CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    return;
                }

                int count = result.Count;
                while (!result.EndOfMessage)
                {
                    if (count >= buffer.Length)
                    {
                        await ws.CloseAsync(WebSocketCloseStatus.InvalidPayloadData, "That's too long", CancellationToken.None);
                        return;
                    }

                    segment = new ArraySegment<byte>(buffer, count, buffer.Length - count);
                    result = await ws.ReceiveAsync(segment, CancellationToken.None);
                    count += result.Count;
                }

                var message = Encoding.UTF8.GetString(buffer, 0, count);

                SalvarTrancricao(message, catalogoPodcast);

                if (IsDelimeter(message))
                {
                    return;
                }
            }
        }

        private void SalvarTrancricao(string message, CatalogoPodcast catalogoPodcast)
        {
            var result = JsonSerializerMemoryStream<ResultRoot>(message);
            if (result.results == null) return;
            if(result.results.Any(x=> x.final))
                catalogosPodcastsDapperRepository.UpdateTrancricao(catalogoPodcast.Id, 
                    result.results
                        .SelectMany(x=> x.alternatives)
                        .Select(x=> x.transcript)
                        .Aggregate((a,b)=> $"{a} {b}"));
        }

        private T JsonSerializerMemoryStream<T>(string json)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            var ser = new DataContractJsonSerializer(typeof(T));
            T obj = (T)ser.ReadObject(stream);
            return obj;
        }

        private bool IsDelimeter(string json)
        {
            return JsonSerializerMemoryStream<ServiceState>(json)?.state == "listening";
        }        
    }
    [DataContract]
    class ServiceState
    {
        [DataMember]
        public string state = "";
    }
    [DataContract]
    public class Alternative
    {
        [DataMember]
        public string transcript { get; set; }
        [DataMember]
        public double confidence { get; set; }

    }

    [DataContract]
    public class Result
    {
        [DataMember]
        public Alternative[] alternatives { get; set; }
        [DataMember]
        public bool final { get; set; }
    }

    [DataContract]
    public class ResultRoot
    {
        [DataMember]
        public Result[] results { get; set; }
        [DataMember]
        public int result_index { get; set; }
        [DataMember]
        public string[] warnings { get; set; }
    }

    class TokenIBM
    {
        public string access_token { get; set; }
    }
}
