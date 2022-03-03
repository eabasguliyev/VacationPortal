using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VacationPortal.DataAccess.Repositories.Abstracts;
using VacationPortal.Models;
using VacationPortal.Web.Areas.Admin.Models.EmployeeVMs;

namespace VacationPortal.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var employees = _unitOfWork.EmployeeRepository.GetAll(includeProperties: "Department,Position").Select(e => new EmployeeVM()
            {
                Id = e.Id,
                Email = e.Email,
                FirstName = e.FirstName,
                LastName = e.LastName,
                DepartmentId = e.DepartmentId,
                Department = e.Department,
                PositionId = e.PositionId,
                Position = e.Position,
                CreatedDate = e.CreatedDate
            });

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var vm = new EmployeeCreateVM();

            vm.EmployeeVM = new EmployeeVM();
            vm.Departments = GetDepartmentListItems();
            vm.Positions = GetPositionListItems();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeVM employeeVM)
        {
            IdentityResult result = null;

            Employee employee = null;

            if (ModelState.IsValid || (ModelState.ErrorCount == 1 && string.IsNullOrWhiteSpace(employeeVM.Password)))
            {

                employee = _mapper.Map<Employee>(employeeVM);

                employee.UserName = employee.Email; // TODO: Fix Here

                employee.CreatedDate = DateTime.Now;

                if (employee != null)
                {
                    result = await _userManager.CreateAsync(employee, employeeVM.Password);
                }
                else
                {
                    return Problem(statusCode: 404);
                }


                if (result.Succeeded)
                {
                    var role = "ADMIN";

                    if (employeeVM.IsAdmin)
                    {
                        await _userManager.AddToRoleAsync(employee, role);
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(employee, role);
                    }

                    return RedirectToAction(nameof(Index));
                }
            }


            for (var i = 0; i < result.Errors.Count(); i++)
            {
                ModelState.AddModelError($"{i + 1}.Error", result.Errors.ElementAt(i).Description);
            }

            var vm = new EmployeeCreateVM();

            vm.EmployeeVM = employeeVM;
            vm.Departments = GetDepartmentListItems();
            vm.Positions = GetPositionListItems();

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var vm = new EmployeeUpdateVM();

            var employeeFromDb = _unitOfWork.EmployeeRepository.Find(id);


            vm.EmployeeVM = _mapper.Map<EmployeeVM>(employeeFromDb);

            vm.EmployeeVM.IsAdmin = await _userManager.IsInRoleAsync(employeeFromDb, "admin");

            if (vm.EmployeeVM == null || vm.EmployeeVM.Id == 0)
            {
                return NotFound();
            }

            vm.Departments = GetDepartmentListItems();

            vm.Positions = GetPositionListItems();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EmployeeVM employeeVM)
        {
            IdentityResult result = null;

            Employee employee = null;

            if (ModelState.IsValid || (ModelState.ErrorCount == 1 && string.IsNullOrWhiteSpace(employeeVM.Password)))
            {

                employee = _unitOfWork.EmployeeRepository.GetFirstOrDefault(p => p.Id == employeeVM.Id);

                _mapper.Map(employeeVM, employee);

                employee.UserName = employee.Email; // TODO: Fix Here

                if (!string.IsNullOrWhiteSpace(employeeVM.Password))
                {
                    employee.PasswordHash = _userManager.PasswordHasher.HashPassword(employee, employeeVM.Password);
                }

                result = await _userManager.UpdateAsync(employee);

                if (result.Succeeded)
                {
                    var role = "ADMIN";

                    if (employeeVM.IsAdmin)
                    {
                        await _userManager.AddToRoleAsync(employee, role);
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(employee, role);
                    }

                    return RedirectToAction(nameof(Index));
                }
            }


            for (var i = 0; i < result.Errors.Count(); i++)
            {
                ModelState.AddModelError($"{i}. Error", result.Errors.ElementAt(i).Description);
            }

            var vm = new EmployeeCreateVM();

            vm.EmployeeVM = employeeVM;
            vm.Departments = GetDepartmentListItems();
            vm.Positions = GetPositionListItems();

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
            var employee = _unitOfWork.EmployeeRepository.Find(id);

            if(employee == null)
                return NotFound();

            // TODO: Find best solution
            string temp = "_" + id.ToString();
            employee.NormalizedUserName += temp;
            employee.NormalizedEmail += temp;

            _unitOfWork.EmployeeRepository.Remove(employee);
            _unitOfWork.Save();

            var employeeId = int.Parse(
                                    HttpContext.User.Claims.FirstOrDefault(c =>
                                    c.Type == ClaimTypes.NameIdentifier).Value);

            if (employeeId == id)
                return RedirectToAction("Logout", "Account", new { area = "Identity" });


            return RedirectToAction(nameof(Index));
        }
    }
}
