﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using ProjectFood.Domain.Entities;
using ProjectFood.Domain.Infrastructure;
using ProjectFood.WebUI.Models;
using ProjectFood.WebUI.Infrastructure;

namespace ProjectFood.WebUI.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }

        [AllowAnonymous]
        public ActionResult Create(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Create(UserViewModel model, string returnUrl)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("'Confirm Password' and 'Password' do not match!", new Exception("Confirm Password Mismatch"));
            }

            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = model.Name,
                    Email = model.Email,
                };
                IdentityResult userResult = await UserManager.CreateAsync(user, model.Password);

                if (userResult.Succeeded)
                {
                    user = await UserManager.FindByNameAsync(model.Name);
                    if (user != null)
                    {
                        IdentityResult roleResult = await UserManager.AddToRoleAsync(user.Id, "Users");
                        if (roleResult.Succeeded)
                        {
                            ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user,
                                DefaultAuthenticationTypes.ApplicationCookie);
                            IdentityHelpers.AuthManager.SignOut();
                            IdentityHelpers.AuthManager.SignIn(new AuthenticationProperties
                            {
                                IsPersistent = false
                            }, ident);
                            return Redirect(returnUrl);
                        }
                    }
                    ModelState.AddModelError("", "There was an error in processing your request. Please try again later!");
                }
                else
                {
                    AddErrorsFromResult(userResult);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            User user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] {"User not found"});
            }
        }

        public async Task<ActionResult> Edit(int id)
        {
            User user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, string email, string password)
        {
            User user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
                user.Email = email;
                IdentityResult validEmail = await UserManager.UserValidator.ValidateAsync(user);
                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                IdentityResult validPass = null;
                if (password != String.Empty)
                {
                    validPass = await UserManager.PasswordValidator.ValidateAsync(password);
                    if (validPass.Succeeded)
                    {
                        user.PasswordHash = UserManager.PasswordHasher.HashPassword(password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                if ((validEmail.Succeeded && validPass == null) ||
                    (validEmail.Succeeded && password != String.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }
            return View(user);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private AppUserManager UserManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<AppUserManager>(); }
        }
    }
}