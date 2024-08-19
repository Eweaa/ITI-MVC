using ITI_MVC.Context;
using ITI_MVC.Interfaces;
using ITI_MVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ITI_MVC.Controllers
{
    public class AccountController : Controller
    {
        ITIContext db = new ITIContext();
        private readonly IUser user;

        public AccountController(IUser _user)
        {
            user = _user;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            user.Create(model.UserName, model.Email, model.Password);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var authenticated = user.Login(login.UserName, login.Password);
                var User = db.Users.SingleOrDefault(u => u.Name == login.UserName);
                if(authenticated)
                {
                    Claim c1 = new Claim(ClaimTypes.Name, login.UserName);
                    Claim c2 = new Claim(ClaimTypes.Email, $"{login.UserName}@iti.gov");
                    //Claim c3 = new Claim(ClaimTypes.Role, $"Student");
                    //Claim c4 = new Claim(ClaimTypes.Role, $"Admin");

                    ClaimsIdentity ci1 = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    ci1.AddClaim(c1);
                    ci1.AddClaim(c2);
                    //ci1.AddClaim(c3);
                    foreach (var role in User.Roles)
                    {
                        Claim c = new Claim(ClaimTypes.Role, role.RoleType);
                        ci1.AddClaim(c);
                    }

                    ClaimsPrincipal cp = new ClaimsPrincipal(ci1);

                    await HttpContext.SignInAsync(cp);
                    return RedirectToAction("Index", "Home");
                    //login
                }
                else
                {
                    ModelState.AddModelError("", "User Name or Password is Incorrect");
                    return View(login);
                    // user does not exist
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
