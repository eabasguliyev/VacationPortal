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
    [Authorize(Policy = "HrDepartment")]
    public class HumanResourceController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HumanResourceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region APICALLS
        
        public IActionResult GetAll(string status)
        {
            Enum.TryParse(status, out VacationApplicationStatus vacationStatus);

            Expression<Func<VacationApplication, bool>> expr = !string.IsNullOrWhiteSpace(status)
                            ? va => va.Status == vacationStatus : null;

            var vacationApplications = _unitOfWork
                    .VacationApplicationRepository
                    .GetAll(expr, includeProperties: "Employee").Select(va => new
                    {
                        Id = va.Id,
                        FirstName = va.Employee.FirstName,
                        LastName = va.Employee.LastName,
                        StartDatetime = va.StartDatetime,
                        DaysOfVacation = va.DaysOfVacation,
                        Status = va.Status.ToString()
                    });

            return Json(new {data = vacationApplications});
        }
        #endregion
    }
}
