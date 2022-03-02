using Microsoft.AspNetCore.Mvc;

namespace VacationPortal.Web.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "VacationInfo");
        }
    }
}
