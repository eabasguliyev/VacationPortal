using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VacationPortal.Models;
using VacationPortal.Web.Areas.Identity.Models;
using VacationPortal.Web.Attributes;

namespace VacationPortal.Web.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        [HttpGet]
        [AnonymousOnly(Role = "Admin", Url1 = "/Admin/Employee", Url2 = "/Employeex/Home")]
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        [AnonymousOnly(Role = "Admin", Url1 = "/Admin/Employee", Url2 = "/Employeex/Home")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AnonymousOnly(Role = "Admin", Url1 = "/Admin/Employee", Url2 = "/Employeex/Home")]
        public async Task<IActionResult> Login(LoginCredential credential)
        {
            if (!ModelState.IsValid)
            {
                return View(credential);
            }

            var user = await _userManager.FindByEmailAsync(credential.Email);

            if(user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, credential.Password);

                if (result)
                {
                    var response = await _signInManager.PasswordSignInAsync(user, credential.Password, true, true);

                    if (response.Succeeded)
                    {
                        var isAdmin = await _userManager.IsInRoleAsync(user, "ADMIN");
                        

                        if (isAdmin)
                        {
                            return RedirectToAction("Index", "Department", new
                            {
                                area = "Admin"
                            });
                        }
                        
                        return RedirectToAction("Index", "Home", new
                        {
                            area = "Employeex"
                        });
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Password is wrong.");
                }
            }
            else
            {
                ModelState.AddModelError("Email", "No account associated this email.");
            }

            return View(credential);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }
    }
}
