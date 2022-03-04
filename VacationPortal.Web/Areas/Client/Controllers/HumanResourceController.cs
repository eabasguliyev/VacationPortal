using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using VacationPortal.DataAccess.Repositories.Abstracts;
using VacationPortal.Models;
using VacationPortal.Web.Areas.Client.Models.HumanResourceVMs;
using VacationPortal.Web.Extensions;

namespace VacationPortal.Web.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Policy = "HrDepartment")]
    public class HumanResourceController : Controller
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

        public HumanResourceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var vacationApplication = _unitOfWork.VacationApplicationRepository.Find(id, noTracking: true, "Employee");

            if (vacationApplication == null) return NotFound();

            var vm = new VacationAppDetailsVM();

            vm.VacationApplication = vacationApplication;

            var posId = _unitOfWork.EmployeeRepository.GetPositionIdByEmployeeId(_employeeId);
            vm.VacationInfo = _unitOfWork.VacationInfoRepository
                .GetFirstOrDefault(vi => vi.PositionId == posId, noTracking: true, includeProperties: "Position");

            return View(vm);
        }

        [HttpGet]
        public IActionResult Accept(int id)
        {
            var vacationApplication = _unitOfWork.VacationApplicationRepository.Find(id);

            if (vacationApplication == null) return NotFound();

            _unitOfWork.VacationApplicationRepository
                .UpdateVacationAppStatus(vacationApplication, VacationApplicationStatus.Approved);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        public IActionResult Decline(int id)
        {
            var vacationApplication = _unitOfWork.VacationApplicationRepository.Find(id);

            if (vacationApplication == null) return NotFound();

            _unitOfWork.VacationApplicationRepository
                .UpdateVacationAppStatus(vacationApplication, VacationApplicationStatus.Declined);

            _unitOfWork.Save();

            return RedirectToAction(nameof(Details), new {id = id});
        }

        #region APICALLS

        public IActionResult GetAll(string status)
        {
            Enum.TryParse(status.Capitalize(), out VacationApplicationStatus vacationStatus);

            Expression<Func<VacationApplication, bool>> expr = !string.IsNullOrWhiteSpace(status)
                            ? va => va.Status == vacationStatus : null;

            var vacationApplications = _unitOfWork
                    .VacationApplicationRepository
                    .GetAll(expr, noTracking:true, includeProperties: "Employee").Select(va => new
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
