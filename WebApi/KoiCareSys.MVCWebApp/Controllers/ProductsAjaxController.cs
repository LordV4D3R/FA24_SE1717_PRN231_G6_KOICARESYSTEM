using Microsoft.AspNetCore.Mvc;

namespace KoiCareSys.MVCWebApp.Controllers
{
    public class ProductsAjaxController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
