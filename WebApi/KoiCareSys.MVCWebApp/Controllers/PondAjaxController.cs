using Microsoft.AspNetCore.Mvc;

namespace KoiCareSys.MVCWebApp.Controllers
{
    public class PondAjaxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
