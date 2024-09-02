using System;
using System.Collections.Generic; // Needed for List<Claim>
using System.Security.Claims; // Needed for ClaimTypes
using System.Threading.Tasks; // Needed for async/await
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class AccountController : Controller
    {
//        public IActionResult Login()
//        {
//            var model = new ApplicationUser
//            {
//                Username = "Arnold",
//                Password = "699869",
//                Email = "arnoldxyx2012@gmail.com"
//            };           
//            return View("Login", model);

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                // Here you would normally verify the user's credentials against a database
                if (model.Username == "Arnold" && model.Password == "699869")
                {
                    // Create the user's claims
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, model.Username)
                    };

                    // Create the identity
                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    // Create the principal
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    // Sign in the user
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        // This is the logout action
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    


        
        
        
        
        
        
        
        
        
        public IActionResult Register()
        {
            var model = new ApplicationUser
            {
                Username = "Arnold",
                Password = "699869",
                Email = "arnoldxyx2012@gmail.com"
            };
            return View("Register", model);
        }

    }
}
