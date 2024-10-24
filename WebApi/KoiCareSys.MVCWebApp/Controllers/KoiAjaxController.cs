using Microsoft.AspNetCore.Mvc;

namespace KoiCareSys.MVCWebApp.Controllers
{
    public class KoiAjaxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
