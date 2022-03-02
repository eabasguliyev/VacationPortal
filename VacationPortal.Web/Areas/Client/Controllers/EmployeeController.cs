using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using VacationPortal.DataAccess.Repositories.Abstracts;
using VacationPortal.Models;

namespace VacationPortal.Web.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private int _employeeId
        {
            get
            {
                var employeeId = int.Parse(
                                    HttpContext.User.Claims.FirstOrDefault(c =>
                                    c.Type == ClaimTypes.NameIdentifier).Value);

                return employeeId;
            }
        }
        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VacationApplication vacationApplication)
        {
            if (!ModelState.IsValid) return View(vacationApplication);

            vacationApplication.CreatedDate = DateTime.Now;
            vacationApplication.EmployeeId = _employeeId;

            _unitOfWork.VacationApplicationRepository.Add(vacationApplication);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        #region APICALLS
        public IActionResult GetAll(string status)
        {
            Enum.TryParse(status, out VacationApplicationStatus vacationStatus);

            IEnumerable<VacationApplication> result = null;

            if (!String.IsNullOrWhiteSpace(status))
            {
                result = _unitOfWork.VacationApplicationRepository.GetAll(va => va.EmployeeId == _employeeId &&
                                                                            va.Status == vacationStatus);
            }
            else
            {
                result = _unitOfWork.VacationApplicationRepository.GetAll(va => va.EmployeeId == _employeeId);
            }

            var vacationApplications = result.Select(va => new
                    {
                        Id = va.Id,
                        StartDatetime = va.StartDatetime,
                        DaysOfVacation = va.DaysOfVacation,
                        Status = va.Status.ToString()
                    });

            return Json(new { data = vacationApplications });
        }
        #endregion
    }
}
