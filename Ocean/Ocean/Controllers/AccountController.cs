using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ocean.Context;
using Ocean.Models;
using Ocean.ViewModels;

namespace Ocean.Controllers
{
    public class AccountController : Controller
    {
        protected UserManager<AppUser> UserManager { get; }
        protected SignInManager<AppUser> SignInManager { get; }
        protected RoleManager<IdentityRole<int>> RoleManager;
        protected OceanDbContext Context { get; }

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole<int>> roleManager, OceanDbContext context)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
            Context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserRole()
        {
            var role = new IdentityRole<int> { Name = "User" };
            await RoleManager.CreateAsync(role);
            return Content("Role [User] added successfully!");
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser(signUpViewModel.Email)
                {
                    Email = signUpViewModel.Email,
                    UserName = signUpViewModel.Login,
                    ProfilePicture = signUpViewModel.ProfilePicture
                };
                var result = await UserManager.CreateAsync(user, signUpViewModel.Password);
                if (result.Succeeded)
                {
                    await SignInManager.PasswordSignInAsync(signUpViewModel.Email, signUpViewModel.Password, true,
                        false);
                    await UserManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(signUpViewModel);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(loginViewModel.Login, loginViewModel.Password,
                    loginViewModel.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong email or password");
                }
            }
            return View(loginViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}