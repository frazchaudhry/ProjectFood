﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ProjectFood.Domain.Concrete;
using ProjectFood.Domain.Entities;
using ProjectFood.Domain.Infrastructure;
using ProjectFood.WebUI.Infrastructure;
using ProjectFood.WebUI.Models;

namespace ProjectFood.WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public async Task<ActionResult> UserProfile(int userId, string activeSection)
        {
            User user = await UserManager.FindByIdAsync(userId);
            if (userId > 0 && user != null)
            {
                var viewModel = new UserProfileViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    AboutMe = user.AboutMe,
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                    Address1 = user.Address1,
                    Address2 = user.Address2,
                    City = user.City,
                    State = user.State,
                    Country = user.Country,
                    ZipCode = user.ZipCode,
                    ImageData = user.ImageData,
                    ImageMimeType = user.ImageMimeType
                };
                using (var db = new EfdbContext())
                {
                    viewModel.Recipes = new List<Recipe>();
                    viewModel.Recipes = db.Recipes.Where(r => r.UserId == userId).ToList();
                }
                ViewBag.ActiveSection = activeSection;
                return View(viewModel);
            }
            
            return View("Error", new[] {"No user was found!"});
        }

        [HttpPost]
        public async Task<ActionResult> EditProfile(UserProfileViewModel userViewModel, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                User user = await UserManager.FindByIdAsync(userViewModel.Id);

                if (user != null)
                {
                    user.Id = userViewModel.Id;
                    user.UserName = userViewModel.UserName;
                    user.Email = userViewModel.Email;
                    user.PasswordHash = userViewModel.PasswordHash;
                    user.FirstName = userViewModel.FirstName;
                    user.LastName = userViewModel.LastName;
                    user.AboutMe = userViewModel.AboutMe;
                    user.Address1 = userViewModel.Address1;
                    user.Address2 = userViewModel.Address2;
                    user.City = userViewModel.City;
                    user.State = userViewModel.State;
                    user.Country = userViewModel.Country;
                    user.ZipCode = userViewModel.ZipCode;
                    user.ImageData = userViewModel.ImageData;
                    user.ImageMimeType = userViewModel.ImageMimeType;

                    if (image != null)
                    {
                        user.ImageMimeType = image.ContentType;
                        user.ImageData = new byte[image.ContentLength];
                        image.InputStream.Read(user.ImageData, 0, image.ContentLength);
                    }

                    using (var db = new EfdbContext())
                    {
                        userViewModel.Recipes = new List<Recipe>();
                        userViewModel.Recipes = db.Recipes.Where(r => r.UserId == userViewModel.Id).ToList();
                    }

                    IdentityResult validationResult = await UserManager.UserValidator.ValidateAsync(user);
                    if (validationResult.Succeeded)
                    {
                        IdentityResult updateResult = await UserManager.UpdateAsync(user);
                        if (updateResult.Succeeded)
                        {
                            ViewBag.ActiveSection = "Profile";
                            return View("UserProfile", userViewModel);
                        }
                        foreach (var error in updateResult.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                    }
                    foreach (var error in validationResult.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
                ModelState.AddModelError("", "There was an error accessing user data. Please try again later!");
            }
            ViewBag.ActiveSection = "EditProfile";
            return View("UserProfile", userViewModel);
        }


        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(UserLoginViewModel details, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = await UserManager.FindAsync(details.Name,
                details.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Invalid name or password.");
                }
                else
                {
                    ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, ident);
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(details);
        }

        [Authorize]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult GoogleLogin(string returnUrl)
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleLoginCallback", new { returnUrl = returnUrl })
            };
            HttpContext.GetOwinContext().Authentication.Challenge(properties, "Google");
            return new HttpUnauthorizedResult();
        }

        [AllowAnonymous]
        public async Task<ActionResult> GoogleLoginCallback(string returnUrl)
        {
            ExternalLoginInfo loginInfo = await AuthManager.GetExternalLoginInfoAsync();
            User user = await UserManager.FindAsync(loginInfo.Login);
            if (user == null)
            {
                user = new User
                {
                    Email = loginInfo.Email,
                    UserName = loginInfo.DefaultUserName
                };
                IdentityResult result = await UserManager.CreateAsync(user);
                if (!result.Succeeded)
                {
                    return View("Error", result.Errors);
                }
                else
                {
                    result = await UserManager.AddLoginAsync(user.Id, loginInfo.Login);
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
            }

            ClaimsIdentity ident =
                await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            ident.AddClaims(loginInfo.ExternalIdentity.Claims);
            AuthManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = false
            }, ident);
            return Redirect(returnUrl ?? "/");
        }

        public async Task<FileContentResult> GetImage(int userId)
        {
            User user = await UserManager.FindByIdAsync(userId);
            if (user != null)
            {
                return File(user.ImageData, user.ImageMimeType);
            }
            return null;
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}