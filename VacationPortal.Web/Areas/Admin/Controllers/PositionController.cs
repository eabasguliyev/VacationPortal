using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
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
        public IActionResult Upsert(int? id)
        {
            var vm = new PositionUpsertVM();
            
            if(id != null && id.HasValue)
            {
                vm.Position = _unitOfWork.PositionRepository.Find(id.Value);

                if(vm.Position == null)
                {
                    return NotFound();
                }
            }
            else
            {
                vm.Position = new Position();
            }

            vm.Departments = _unitOfWork.DepartmentRepository.GetAll().Select(d => new SelectListItem()
            {
                Text = d.FullName,
                Value = d.Id.ToString(),
            });

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Position position)
        {
            if (!ModelState.IsValid)
            {
                var vm = new PositionUpsertVM();
                vm.Position = position;

                vm.Departments = _unitOfWork.DepartmentRepository.GetAll().Select(d => new SelectListItem()
                {
                    Text = d.FullName,
                    Value = d.Id.ToString(),
                });
                return View(vm);
            }

            if(position.Id != 0)
            {
                var positionFromDb = _unitOfWork.PositionRepository.Find(position.Id, noTracking: true);
                position.CreatedDate = positionFromDb.CreatedDate;
                _unitOfWork.PositionRepository.Update(position);
            }
            else
            {
                position.CreatedDate = DateTime.Now;
                _unitOfWork.PositionRepository.Add(position);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
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
