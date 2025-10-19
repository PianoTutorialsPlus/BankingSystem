using System.Net.Http;
using System.Net.Http.Headers;

namespace BankingSystem.UI.Services.Base
{
    public partial class Client : IClient
    {
        public Client()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7178/") // change if API runs elsewhere
            };
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public HttpClient HttpClient => _httpClient;
    }
}
