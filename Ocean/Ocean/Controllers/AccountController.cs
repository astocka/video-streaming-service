using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                    MyProfiles = new List<UserProfile>
                    {
                        new UserProfile()
                        {
                            Name = signUpViewModel.Login,
                            AppUserId = 1,
                            ProfilePictureId = 1
                        } 
                    }
                };
                var result = await UserManager.CreateAsync(user, signUpViewModel.Password);
                if (result.Succeeded)
                {
                    await SignInManager.PasswordSignInAsync(signUpViewModel.Email, signUpViewModel.Password, true,
                        false);

                    await UserManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("MyAccount", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(signUpViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UserRole()
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);
            await UserManager.AddToRoleAsync(user, "User");
            return Content("done");
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
                    return RedirectToAction("MyAccount", "Account");
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

        [HttpGet]
        [Authorize]
        public IActionResult ManageProfiles()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyAccount()
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);
            if (user != null)
            {
                var profile = await Context.UserProfiles.FirstOrDefaultAsync(x => x.AppUserId == user.Id);
                var pic = await Context.UserProfiles.Include(p => p.ProfilePicture).FirstOrDefaultAsync(x => x.ProfilePictureId == profile.ProfilePictureId);
                var thumb = pic.ProfilePicture.ThumbnailFilePath;
                var picture = pic.ProfilePicture.FilePath;

                ViewData["Name"] = profile.Name;
                ViewData["Thumb"] = thumb.ToString();
                ViewData["Picture"] = picture.ToString();
            }
            return View(user);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                var result =
                    await UserManager.ChangePasswordAsync(user, viewModel.CurrentPassword, viewModel.NewPassword);

                if (result.Succeeded)
                {
                    await SignInManager.SignOutAsync();
                    return RedirectToAction("LogIn", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Wrong login or password");
                }

            }
            return View(viewModel);
        }
    }
}