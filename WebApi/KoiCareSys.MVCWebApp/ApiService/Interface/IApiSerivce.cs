﻿namespace KoiCareSys.MVCWebApp.ApiService.Interface
{
    public interface IApiService
    {
        Task<T> GetAsync<T>(string endpoint);
        Task<T> PostAsync<T>(string endpoint, object data);
        Task<T> DeleteAsync<T>(string endpoint);
        Task<T> PutAsync<T>(string endpoint, object data);
    }
}
