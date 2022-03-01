using Microsoft.AspNetCore.Mvc;
using System;
using VacationPortal.DataAccess.Repositories.Abstracts;
using VacationPortal.Models;

namespace VacationPortal.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(departments);
        }

        //[HttpGet("[area]/[controller]/Create")]
        //[HttpGet("[area]/[controller]/Edit/{id}")]
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Department department = null;

            if(id != null && id.HasValue)
            {
                department = _unitOfWork.DepartmentRepository.Find(id.Value);

                if(department == null)
                {
                    return NotFound();
                }
            }
            else
            {
                department = new Department();
            }
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            if(department.Id != 0)
            {
                _unitOfWork.DepartmentRepository.Update(department);
            }
            else
            {
                department.CreatedDate = DateTime.Now;
                _unitOfWork.DepartmentRepository.Add(department);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var department = _unitOfWork.DepartmentRepository.Find(id);

            if(department == null)
                return NotFound();

            _unitOfWork.DepartmentRepository.Remove(department);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
