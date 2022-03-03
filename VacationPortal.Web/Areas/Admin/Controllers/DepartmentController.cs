using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using VacationPortal.DataAccess.Repositories.Abstracts;
using VacationPortal.Models;

namespace VacationPortal.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin")]
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }


            department.CreatedDate = DateTime.Now;
            _unitOfWork.DepartmentRepository.Add(department);

            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            Department department = _unitOfWork.DepartmentRepository.Find(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            var departmentFromDb = _unitOfWork.DepartmentRepository.Find(department.Id, noTracking: true);

            department.CreatedDate = departmentFromDb.CreatedDate;

            _unitOfWork.DepartmentRepository.Update(department);

            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var department = _unitOfWork.DepartmentRepository.Find(id);

            if(department == null)
                return NotFound();

            var employees = _unitOfWork.EmployeeRepository.GetAll(e => e.DepartmentId == id);

            _unitOfWork.EmployeeRepository.RemoveRange(employees);
            _unitOfWork.DepartmentRepository.Remove(department);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
