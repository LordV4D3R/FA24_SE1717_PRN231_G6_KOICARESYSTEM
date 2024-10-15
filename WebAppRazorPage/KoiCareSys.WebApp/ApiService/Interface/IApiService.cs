namespace KoiCareSys.WebApp.ApiService.Interface
{
    public interface IApiService
    {
        Task<T> GetAsync<T>(string endpoint);
        Task<T> PostAsync<T>(string endpoint, object data);
        // Thêm các phương thức khác nếu cần
    }
}
