using Microsoft.AspNetCore.Mvc;
using VacationPortal.DataAccess.Repositories.Abstracts;

namespace VacationPortal.Web.Areas.Client.Controllers
{
    [Area("Client")]
    public class VacationInfoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VacationInfoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var vacationInfos = _unitOfWork.VacationInfoRepository.GetAll(includeProperties: "Position");
            return View(vacationInfos);
        }
    }
}
