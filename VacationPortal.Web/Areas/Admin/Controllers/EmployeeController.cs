using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;
using VacationPortal.DataAccess.Repositories.Abstracts;
using VacationPortal.Models;
using VacationPortal.Web.Areas.Admin.Models.EmployeeVMs;

namespace VacationPortal.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        //[BindProperty]
        //public EmployeeUpsertVM UpsertVM { get; set; }
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
        public IActionResult Upsert(int? id)
        {
            var vm = new EmployeeUpsertVM();
            
            if(id != null && id.HasValue)
            {
                var employeeFromDb = _unitOfWork.EmployeeRepository.Find(id.Value);


                vm.EmployeeVM = _mapper.Map<EmployeeVM>(employeeFromDb);

                if(vm.EmployeeVM == null || vm.EmployeeVM.Id == 0)
                {
                    return NotFound();
                }
            }
            else
            {
                vm.EmployeeVM = new EmployeeVM();
            }

            // TODO: DRY
            vm.Departments = _unitOfWork.DepartmentRepository.GetAll().Select(d => new SelectListItem()
            {
                Text = d.FullName,
                Value = d.Id.ToString(),
            });

            vm.Positions = _unitOfWork.PositionRepository.GetAll().Select(p => new SelectListItem()
            {
                Text = p.Name,
                Value = p.Id.ToString(),
            });

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(EmployeeVM employeeVM)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }


            IdentityResult result = null;

            if (employeeVM.Id != 0)
            {
                var employeeFromDb = _unitOfWork.EmployeeRepository.GetFirstOrDefault(p => p.Id == employeeVM.Id);

                _mapper.Map<EmployeeVM, Employee>(employeeVM, employeeFromDb);

                employeeFromDb.UserName = employeeFromDb.Email; // TODO: Fix Here

                result = await _userManager.UpdateAsync(employeeFromDb);
            }
            else
            {
                var employee = _mapper.Map<Employee>(employeeVM);

                employee.UserName = employee.Email; // TODO: Fix Here

                employee.CreatedDate = DateTime.Now;

                if(employee != null)
                {
                    result = await _userManager.CreateAsync(employee, employeeVM.Password);
                }
                else
                {
                    return Problem(statusCode: 404);
                }
            }

            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            for (var i = 0; i < result.Errors.Count(); i++)
            {
                ModelState.AddModelError("Error" + i, result.Errors.ElementAt(i).Description);
            }

            var vm = new EmployeeUpsertVM();

            vm.EmployeeVM = employeeVM;

            vm.Departments = _unitOfWork.DepartmentRepository.GetAll().Select(d => new SelectListItem()
            {
                Text = d.FullName,
                Value = d.Id.ToString(),
            });

            vm.Positions = _unitOfWork.PositionRepository.GetAll().Select(p => new SelectListItem()
            {
                Text = p.Name,
                Value = p.Id.ToString(),
            });

            return View(vm);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = _unitOfWork.EmployeeRepository.Find(id);

            if(employee == null)
                return NotFound();

            _unitOfWork.EmployeeRepository.Remove(employee);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
