using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using VacationPortal.DataAccess.Repositories.Abstracts;
using VacationPortal.Models;
using VacationPortal.Web.Areas.Client.Models.EmployeeVMs;
using VacationPortal.Web.Extensions;

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
            if (_unitOfWork.VacationApplicationRepository.IsExistPendingApplicationByEmployeeId(_employeeId))
            {
                TempData["error"] = "You already created application.";
                return RedirectToAction(nameof(Index));
            }


            var vm = new VacationAppCreateVM();
            vm.VacationApplication = new VacationApplication()
            {
                StartDatetime = DateTime.Now,
                DaysOfVacation = 1
            };

            var posId = _unitOfWork.EmployeeRepository.GetPositionIdByEmployeeId(_employeeId);
            vm.VacationInfo = _unitOfWork.VacationInfoRepository
                .GetFirstOrDefault(vi => vi.PositionId == posId, noTracking: true, includeProperties: "Position");


            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VacationApplication vacationApplication)
        {
            if (!ModelState.IsValid)
            {
                var vm = new VacationAppCreateVM();
                vm.VacationApplication = vacationApplication;

                var posId = _unitOfWork.EmployeeRepository.GetPositionIdByEmployeeId(_employeeId);
                vm.VacationInfo = _unitOfWork.VacationInfoRepository
                    .GetFirstOrDefault(vi => vi.PositionId == posId, noTracking: true, includeProperties: "Position");

                return View(vm);
            }

            vacationApplication.CreatedDate = DateTime.Now;
            vacationApplication.EmployeeId = _employeeId;

            _unitOfWork.VacationApplicationRepository.Add(vacationApplication);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
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


        #region APICALLS
        public IActionResult GetAll(string status)
        {
            Enum.TryParse(status.Capitalize(), out VacationApplicationStatus vacationStatus);

            IEnumerable<VacationApplication> result = null;

            var statusIsEmpty = string.IsNullOrWhiteSpace(status);

            var expression = 
                GetExpressionByCondition(statusIsEmpty, _employeeId, vacationStatus);

            result = _unitOfWork.VacationApplicationRepository.GetAll(expression, noTracking: true);

            var vacationApplications = result.Select(va => new
                    {
                        Id = va.Id,
                        StartDatetime = va.StartDatetime,
                        DaysOfVacation = va.DaysOfVacation,
                        Status = va.Status.ToString()
                    });

            return Json(new { data = vacationApplications });
        }

        private Expression<Func<VacationApplication, bool>> GetExpressionByCondition(bool status, 
            int employeeId, VacationApplicationStatus vacationStatus)
        {
            Dictionary<bool, Expression<Func<VacationApplication, bool>>> expressionPairs = 
                new Dictionary<bool, Expression<Func<VacationApplication, bool>>>();
            expressionPairs.Add(false, va => va.EmployeeId == employeeId && va.Status == vacationStatus);
            expressionPairs.Add(true, va => va.EmployeeId == employeeId);

            return expressionPairs[status];
        }
        #endregion
    }
}
