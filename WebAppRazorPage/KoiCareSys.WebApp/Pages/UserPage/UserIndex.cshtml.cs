using KoiCareSys.WebApp.ApiService.Interface;
using KoiCareSys.WebApp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;

namespace KoiCareSys.WebApp.Pages.UserPage
{
    public class UserIndexModel : PageModel
    {
        private readonly IApiService _apiService;

        public UserIndexModel(IApiService apiService)
        {
            _apiService = apiService;
        }

        public List<UserDto> Users { get; set; }
        public async Task OnGetAsync()
        {
            try
            {
                Users = await _apiService.GetAsync<List<UserDto>>("api/users");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching users: {ex.Message}");
            }
        }
    }
}
