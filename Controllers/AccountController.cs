using System;
using System.Collections.Generic;           // Needed for List<Claim>
using System.Security.Claims;               // Needed for ClaimTypes
using System.Threading.Tasks;               // Needed for async/await
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using MvcMovie.Data;
using Microsoft.EntityFrameworkCore;



namespace MvcMovie.Controllers
{
    public class AccountController : Controller
    {
        private readonly MvcMovieContext _context;
        public AccountController(MvcMovieContext context)
        {
            _context = context;
        }

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
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);

                if (user != null)
                {
                    // 存储用户信息在 Session 中
                    HttpContext.Session.SetString("Username", user.Username);

                    return RedirectToAction("Index", "Home");
                }

                // Here you would normally verify the user's credentials against a database
                // if (model.Username == "Arnold" && model.Password == "699869")
                // {
                //     // Create the user's claims
                //     var claims = new List<Claim>
                //     {
                //         new Claim(ClaimTypes.Name, model.Username)
                //     };

                //     // Create the identity
                //     var claimsIdentity = new ClaimsIdentity(
                //         claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //     // Create the principal
                //     var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                //     // Sign in the user
                //     await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                //     return RedirectToAction("Index", "Home");
                // }

                ModelState.AddModelError(string.Empty, "Invalid login attempt，请检查用户名和密码.");
            }

            return View(model);
        }

        // This is the logout action
        

        public IActionResult SecurePage()
        {
            // 检查用户是否已登录
            var username = HttpContext.Session.GetString("Username");
            if (string.IsNullOrEmpty(username))
            {
                return RedirectToAction("Login", "Account");
            }

            // 用户已登录，继续处理请求
            return View();
        }

        public IActionResult Logout()
        {
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
  
        // 显示注册页面
        public IActionResult Register()
        {
            return View();
        }

        // 处理注册提交
        [HttpPost]
        public async Task<IActionResult> Register(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                // 检查用户名是否已存在
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == model.Username);
            
                if (existingUser != null)
                {
                // 用户名已存在，返回错误
                    ModelState.AddModelError(string.Empty, "用户名已存在，请选择其他用户名。");
                    return View(model);
                }

                // 保存新用户到数据库
                _context.Users.Add(model);
                await _context.SaveChangesAsync();

                // 注册成功后，直接登录用户
                HttpContext.Session.SetString("Username", model.Username);
                return RedirectToAction("Index", "Home"); // 重定向到主页
            }

            return View(model); // 如果输入无效，返回注册页面
        }
    }
}