using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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
        public IActionResult Create()
        {
            var vm = new VacationInfoCreateVM();

            vm.VacationInfo = new VacationInfo();

            vm.Positions = GetPositionListItems();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VacationInfo vacationInfo)
        {
            if (ModelState.IsValid)
            {
                var vacationForPositionIsExist = _unitOfWork
                                            .VacationInfoRepository
                                            .GetFirstOrDefault(vi => vi.PositionId == vacationInfo.PositionId, true);

                if (vacationForPositionIsExist == null)
                {
                    vacationInfo.CreatedDate = DateTime.Now;

                    _unitOfWork.VacationInfoRepository.Add(vacationInfo);

                    _unitOfWork.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Position", "Vacation already exists for this position");
                }
            }

            var vm = new VacationInfoCreateVM();

            vm.VacationInfo = vacationInfo;

            vm.Positions = GetPositionListItems();

            return View(vm);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var vm = new VacationInfoUpdateVM();

            vm.VacationInfo = _unitOfWork.VacationInfoRepository.Find(id);

            if (vm.VacationInfo == null)
            {
                return NotFound();
            }

            vm.Positions = GetPositionListItems();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(VacationInfo vacationInfo)
        {
            if (ModelState.IsValid)
            {
                var vacationForPositionIsExist = _unitOfWork
                                           .VacationInfoRepository
                                           .GetFirstOrDefault(vi => vi.PositionId == vacationInfo.PositionId, true);

                if (vacationForPositionIsExist == null || vacationInfo.Id == vacationForPositionIsExist.Id)
                {
                    var vacationInfoFromDb = _unitOfWork.VacationInfoRepository.Find(vacationInfo.Id, noTracking: true);

                    vacationInfo.CreatedDate = vacationInfoFromDb.CreatedDate;
                    vacationInfo.ModelStatus = vacationInfoFromDb.ModelStatus;

                    _unitOfWork.VacationInfoRepository.Update(vacationInfo);
                    _unitOfWork.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Position", "Vacation already exists for this position");
                }
            }

            var vm = new VacationInfoUpdateVM();

            vm.VacationInfo = vacationInfo;

            vm.Positions = GetPositionListItems();

            return View(vm);
        }

        private IEnumerable<SelectListItem> GetPositionListItems()
        {
            return _unitOfWork.PositionRepository.GetAll().Select(d => new SelectListItem()
            {
                Text = d.Name,
                Value = d.Id.ToString(),
            });
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
