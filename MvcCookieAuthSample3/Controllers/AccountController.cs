using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcCookieAuthSample2.Models;

namespace MvcCookieAuthSample2.Controllers
{
    public class AccountController : Controller
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        //private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly TestUserStore _users;

        public AccountController(TestUserStore users)
        {
            _users = users;
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }


        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            //if (ModelState.IsValid)
            //{
            //    var user = new ApplicationUser
            //    {
            //        UserName = registerViewModel.Email,
            //        Email = registerViewModel.Email,
            //        NormalizedUserName = registerViewModel.Email
            //    };
            //    var result = await _userManager.CreateAsync(user, registerViewModel.Password);
            //    if (result.Succeeded)
            //    {
            //        await _signInManager.SignInAsync(user, isPersistent: true);
            //        return RedirectToLocal(returnUrl);
            //    }
            //    else
            //    {
            //        AddErrors(result);
            //    }
            //}

            return View();
        }

        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                ViewData["ReturnUrl"] = returnUrl;
                var user = _users.FindByUsername(loginViewModel.UserName);
                if (user == null)
                {
                    ModelState.AddModelError(nameof(loginViewModel.UserName), "UserName not exists");
                }

                if (_users.ValidateCredentials(loginViewModel.UserName, loginViewModel.Password))
                {
                    var props = new AuthenticationProperties()
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))
                    };

                    await Microsoft.AspNetCore.Http.AuthenticationManagerExtensions.SignInAsync(
                          HttpContext,
                          user.SubjectId,
                          user.Username,
                          props);

                    return Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(nameof(loginViewModel.Password), "password wrong");
                }
            }

            return View();
        }

        public IActionResult MakeLogin()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"adminUser"),
                new Claim( ClaimTypes.Role, "admin")
            };

            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));

            return Ok();
        }

        public async Task<IActionResult> Logout()
        {
            //await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}