using Microsoft.AspNetCore.Mvc;
using VacationPortal.DataAccess.Repositories.Abstracts;

namespace VacationPortal.Web.Areas.Client.Controllers
{
    public class VacationInfoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VacationInfoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
