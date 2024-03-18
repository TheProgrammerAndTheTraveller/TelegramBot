using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
namespace TelegramInteraction
{
    public class CatApi
    {
        private readonly HttpClient _httpClient;

        public CatApi(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> GetCatUri()
        {
            var result = await _httpClient.GetStringAsync("https://api.thecatapi.com/v1/images/search");

            var json = JsonSerializer.Deserialize<CatResponse[]>(result);

            return json[0].url;
        }

        private record CatResponse(string url);

    }

}
