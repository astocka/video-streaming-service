using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ocean.Context;
using Ocean.Models;
using Ocean.ViewModels;

namespace Ocean.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
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
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
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
        public async Task<IActionResult> ManageProfiles()
        {
            var user = await UserManager.GetUserAsync(HttpContext.User);
            var userProfiles = await Context.UserProfiles.Where(a => a.AppUserId == user.Id).Include(p => p.ProfilePicture).ToListAsync();
            return View(userProfiles);
        }

        [HttpGet]
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

        [HttpGet]
        public async Task<IActionResult> CreateProfile()
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            var userProfiles = await Context.UserProfiles.Where(a => a.AppUserId == user.Id).Include(p => p.ProfilePicture).ToListAsync();
            ViewData["user-id"] = user.Id;
            ViewBag.DefaultImage = "~/images/Profile/images/Profile/icons8-anonymous-mask-96.png";

            if (userProfiles.Count == 3)
            {
                ViewData["max-profiles"] = "You can create up to three profiles for one account.";
                return View();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile(ProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(User.Identity.Name);
                await Context.UserProfiles.ToListAsync();
                var newProfile = new UserProfile()
                {
                    Name = viewModel.Name,
                    AppUserId = viewModel.AppUserId,
                    ProfilePictureId = 1
                };
                Context.Add(newProfile);

                user.MyProfiles.Add(newProfile);

                var result = Context.SaveChanges();
                if (result == 1)
                {
                    return RedirectToAction("ManageProfiles", "Account");
                }
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> SelectProfile()
        {
            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            var profiles = await Context.UserProfiles.Where(p => p.AppUserId == user.Id).ToListAsync();
            await Context.ProfilePictures.ToListAsync();
            return View(profiles);
        }

        [HttpPost]
        public async Task<IActionResult> SelectProfile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            var allProfiles = await Context.UserProfiles.Where(u => u.AppUserId == user.Id).ToListAsync();
            var activeProfile = await Context.UserProfiles.FirstOrDefaultAsync(i => i.UserProfileId == id);

            foreach (var profile in allProfiles)
            {
                profile.IsActive = false;
            }

            activeProfile.IsActive = true;

            foreach (var userProfile in allProfiles)
            {
                Context.Update(userProfile);
            }
            Context.Update(activeProfile);
            var result = Context.SaveChanges();
            if (result == 1)
            {
                return RedirectToAction("Index", "Video");
            }
            return RedirectToAction("SelectProfile", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            ViewData["user-id"] = user.Id;
            ViewBag.UserProfileId = id;

            ViewBag.Pictures = await Context.ProfilePictures.ToListAsync();
            ViewBag.UserProfilesCount = Context.UserProfiles.Count(p => p.AppUserId == id);

            var userProfile = await Context.UserProfiles.FirstOrDefaultAsync(u => u.UserProfileId == id);
            return View(userProfile);
        }

        [HttpPost]
        [Route("Account/EditProfile/{id}")]
        public async Task<IActionResult> EditProfile(int? id, [Bind("UserProfileId, AppUserId, Name, ProfilePictureId")]UserProfile userProfile)
        {
            if (ModelState.IsValid && id != null)
            {
                var profile = await Context.UserProfiles.FirstOrDefaultAsync(p => p.UserProfileId == id);
                profile.Name = userProfile.Name;
                profile.AppUserId = userProfile.AppUserId;
                profile.ProfilePictureId = userProfile.ProfilePictureId;

                Context.Update(profile);

                var result = Context.SaveChanges();
                if (result == 1)
                {
                    return RedirectToAction("ManageProfiles", "Account");
                }
            }

            return View(userProfile);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProfile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var profile = await Context.UserProfiles.Include(pic => pic.ProfilePicture).FirstOrDefaultAsync(p => p.UserProfileId == id);
            return View(profile);
        }

        [HttpPost]
        [Route("Account/DeleteConfirmProfile/{id}")]
        public async Task<IActionResult> ConfirmDeleteProfile(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var profile = await Context.UserProfiles.FirstOrDefaultAsync(p => p.UserProfileId == id);

            Context.Remove(profile);
            var result = Context.SaveChanges();
            if (result == 1)
            {
                return RedirectToAction("ManageProfiles", "Account");
            }

            return RedirectToAction("DeleteProfile", "Account");
        }
    }
}