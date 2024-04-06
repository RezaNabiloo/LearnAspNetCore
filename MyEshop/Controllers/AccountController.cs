using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using MyEshop.Data.Repositories;
using MyEshop.Models;
using System.Security.Claims;

namespace MyEshop.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository _userRepository;
        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVieModel dataModel)
        {
            if (!ModelState.IsValid)
            {
                return View(dataModel);
            }
            if (_userRepository.IsExistsUserByEmail(dataModel.Email.ToLower()))
            {
                ModelState.AddModelError("Email", "کاربر با این ایمیل قبلا ثبت نام نموده است.");
                return View(dataModel);
            }


            User user = new User()
            {
                Email = dataModel.Email.ToLower(),
                Password = dataModel.Password,
                IsAdmin = false
            };
            _userRepository.AddUser(user);
            return View("Successregister", dataModel);
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVieModel dataModel)
        {
            if (!ModelState.IsValid)
            {
                return View(dataModel);
            }

            var user = _userRepository.GetUserForLogin(dataModel.Email.ToLower(), dataModel.Password);
            if (user == null)
            {
                ModelState.AddModelError("Email", "اططلاعات صحیح نیست");
                return View(dataModel);

            }



            var climes = new List<Claim>
            {
             new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString())    ,
             new Claim(ClaimTypes.Name, user.Email)
            };

            var identity = new ClaimsIdentity(climes, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = dataModel.RememberMe
            };
            HttpContext.SignInAsync(principal, properties);

            return RedirectToAction("Index", "Home");
        }



        public IActionResult Logout(){

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
