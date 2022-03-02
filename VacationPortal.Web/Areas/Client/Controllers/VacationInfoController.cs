using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using VacationPortal.DataAccess.Repositories.Abstracts;
using VacationPortal.Models;
using VacationPortal.Web.Areas.Client.Models.VacationInfoVMs;

namespace VacationPortal.Web.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Policy = "HrDepartment")]
    public class VacationInfoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public VacationInfoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vacationInfos = _unitOfWork.VacationInfoRepository.GetAll(includeProperties: "Position");
            return View(vacationInfos);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            var vm = new VacationInfoUpsertVM();

            if(id != null && id.HasValue)
            {
                vm.VacationInfo = _unitOfWork.VacationInfoRepository.Find(id.Value);

                if(vm.VacationInfo == null)
                {
                    return NotFound();
                }
            }
            else
            {
                vm.VacationInfo = new VacationInfo();
            }

            vm.Positions = _unitOfWork.PositionRepository.GetAll().Select(p =>
                new SelectListItem()
                {
                    Text = p.Name,
                    Value = p.Id.ToString(),
                }
            );
            return View(vm);
        }

        [HttpPost]
        public IActionResult Upsert(VacationInfo vacationInfo)
        {
            if (!ModelState.IsValid)
            {
                var vm = new VacationInfoUpsertVM();

                vm.VacationInfo = vacationInfo;

                vm.Positions = _unitOfWork.PositionRepository.GetAll().Select(p =>
                    new SelectListItem()
                    {
                        Text = p.Name,
                        Value = p.Id.ToString(),
                    });

                return View(vm);
            }

            if (vacationInfo.Id != 0)
            {
                var vacationInfoFromDb = _unitOfWork.VacationInfoRepository.Find(vacationInfo.Id, noTracking: true);

                vacationInfo.CreatedDate = vacationInfoFromDb.CreatedDate;
                vacationInfo.ModelStatus = vacationInfoFromDb.ModelStatus;

                _unitOfWork.VacationInfoRepository.Update(vacationInfo);
            }
            else
            {
                vacationInfo.CreatedDate = DateTime.Now;

                _unitOfWork.VacationInfoRepository.Add(vacationInfo);
            }

            _unitOfWork.Save();
            
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var vacationInfo = _unitOfWork.VacationInfoRepository.Find(id);

            if (vacationInfo == null)
                return NotFound();

            _unitOfWork.VacationInfoRepository.Remove(vacationInfo);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
