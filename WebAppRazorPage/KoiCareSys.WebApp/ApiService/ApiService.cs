using KoiCareSys.WebApp.ApiService.Interface;

namespace KoiCareSys.WebApp.ApiService
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ApiService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var client = _clientFactory.CreateClient("MyAPI");
            var response = await client.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T> PostAsync<T>(string endpoint, object data)
        {
            var client = _clientFactory.CreateClient("MyAPI");
            var response = await client.PostAsJsonAsync(endpoint, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}
