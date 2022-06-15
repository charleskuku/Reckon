using ReckonStringMatching.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Encodings;
using System.Text;
using System.Threading;

namespace ReckonStringMatching.Services.Integration
{
    public class IntegrationService
    {

        private readonly HttpClient _client;
        public IntegrationService(HttpClient client) { 
            _client = client;
        }

        public async Task<TextToSearchModel> GetTextToSearch(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "test2/textToSearch");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                response.EnsureSuccessStatusCode();
                return stream.ReadAndDeserializeFromJson<TextToSearchModel>();
            }
        }

        public async Task<SubTextModel> GetSubTexts(CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "test2/subTexts");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();
                response.EnsureSuccessStatusCode();
                return stream.ReadAndDeserializeFromJson<SubTextModel>();
            }
        }

        public async Task<SearchResultModel> PostResults(StringSearchModel model,CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "test2/submitResults");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead,
                cancellationToken))
            {
                response.EnsureSuccessStatusCode();
                var stream = await response.Content.ReadAsStringAsync();

                var result  = JsonSerializer.Deserialize<SearchResultModel>(stream,
                 new JsonSerializerOptions
                 {

                     PropertyNamingPolicy = JsonNamingPolicy.CamelCase

                 });
                return result;
            }
            
        }

    }
}
