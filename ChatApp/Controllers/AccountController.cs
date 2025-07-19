using ChatApp.Models;
using ChatApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController
            (UserManager<User> _userManager,
            SignInManager<User> signInManager)
        {
            userManager = _userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserVM newUserVM)
        {
            if (ModelState.IsValid)
            {
                User userModel = new User();
                userModel.UserName = newUserVM.UserName;
                userModel.PasswordHash = newUserVM.Password;

                IdentityResult result = await userManager.CreateAsync(userModel, newUserVM.Password);

                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(userModel, isPersistent: false); 
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }
            return View(newUserVM);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM userVM)
        {
            if (ModelState.IsValid)
            {
                // chekc
                User userModel = await userManager.FindByNameAsync(userVM.UserName);
                if (userModel is not null)
                {
                    bool find = await userManager.CheckPasswordAsync(userModel, userVM.Password);
                    if (find == true)
                    {
                        // make cookie
                        List<Claim> claims = new List<Claim>(); 
                        await signInManager.SignInWithClaimsAsync(userModel,
                            userVM.RemeberMe, claims);
                        return RedirectToAction("Index", "Home");
                    }
                }
                ModelState.AddModelError("", "User and Password is invalid");
            }
            return View(userVM);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
