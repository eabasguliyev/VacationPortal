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
                vm.Position = _unitOfWork.PositionRepository.GetFirstOrDefault(d => d.Id == id);

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
                return View(position);
            }

            if(position.Id != 0)
            {
                var positionFromDb = _unitOfWork.PositionRepository.GetFirstOrDefault(p => p.Id == position.Id);
                positionFromDb.Name = position.Name;
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
            var position = _unitOfWork.PositionRepository.GetFirstOrDefault(d => d.Id == id);

            if(position == null)
                return NotFound();

            _unitOfWork.PositionRepository.Remove(position);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
