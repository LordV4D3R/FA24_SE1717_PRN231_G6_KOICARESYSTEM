using KoiCareSys.MVCWebApp.ApiService.Interface;

namespace KoiCareSys.MVCWebApp.ApiService
{
    public class ApiService : IApiService
    {
        private IHttpClientFactory _clientFactory;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _clientFactory = httpClientFactory;
        }

        public async Task<T> DeleteAsync<T>(string endpoint)
        {
            var client = _clientFactory.CreateClient("MyAPI");
            var response = await client.DeleteAsync(endpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
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


        public async Task<T> PutAsync<T>(string endpoint, object data)
        {
            var client = _clientFactory.CreateClient("MyAPI");
            var response = await client.PutAsJsonAsync(endpoint, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}
