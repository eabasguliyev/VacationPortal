using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using VacationPortal.DataAccess.Repositories.Abstracts;
using VacationPortal.Models;
using VacationPortal.Web.Areas.Admin.Models.PositionVMs;

namespace VacationPortal.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PositionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PositionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var positions = _unitOfWork.PositionRepository.GetAll();
            return View(positions);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new PositionCreateVM();
            vm.Position = new Position();

            vm.Departments = GetDepartmentListItems();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Position position)
        {
            if (ModelState.IsValid)
            {
                var positionIsExist = _unitOfWork.PositionRepository.GetFirstOrDefault(p => p.Name == position.Name);

                if(positionIsExist == null)
                {
                    position.CreatedDate = DateTime.Now;
                    _unitOfWork.PositionRepository.Add(position);

                    _unitOfWork.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "Position already exists.");
                }
            }

            var vm = new PositionCreateVM();
            vm.Position = position;

            vm.Departments = GetDepartmentListItems();

            return View(vm);
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var vm = new PositionUpdateVM();

            vm.Position = _unitOfWork.PositionRepository.Find(id);

            if (vm.Position == null)
            {
                return NotFound();
            }

            vm.Departments = GetDepartmentListItems();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Position position)
        {
            if (ModelState.IsValid)
            {
                var positionIsExist = _unitOfWork.PositionRepository.GetFirstOrDefault(p => p.Name == position.Name);

                if(positionIsExist == null)
                {
                    var positionFromDb = _unitOfWork.PositionRepository.Find(position.Id, noTracking: true);

                    position.CreatedDate = positionFromDb.CreatedDate;

                    _unitOfWork.PositionRepository.Update(position);
                    _unitOfWork.Save();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "Position already exists.");
                }
            }

            var vm = new PositionUpdateVM();

            vm.Position = position;

            vm.Departments = GetDepartmentListItems();

            return View(vm);
        }

        private IEnumerable<SelectListItem> GetDepartmentListItems()
        {
            return _unitOfWork.DepartmentRepository.GetAll().Select(d => new SelectListItem()
            {
                Text = d.FullName,
                Value = d.Id.ToString(),
            });
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            var position = _unitOfWork.PositionRepository.Find(id);

            if(position == null)
                return NotFound();

            var employees = _unitOfWork.EmployeeRepository.GetAll(e => e.PositionId == id);
            var vacationInfos = _unitOfWork.VacationInfoRepository.GetAll(vi => vi.PositionId == id);

            _unitOfWork.EmployeeRepository.RemoveRange(employees);
            _unitOfWork.VacationInfoRepository.RemoveRange(vacationInfos);
            _unitOfWork.PositionRepository.Remove(position);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
