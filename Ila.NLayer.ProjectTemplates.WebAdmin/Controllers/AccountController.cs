using System.Diagnostics;
using System.Security.Claims;
using Ila.NLayer.ProjectTemplates.Core.Models.Account;
using Ila.NLayer.ProjectTemplates.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Ila.NLayer.ProjectTemplates.WebAdmin.Controllers
{
    public class AccountController: WebApi.Controllers.ControllerBase
    {
        private const string SessionKeyName = "_UserName";

        private UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager,
                                 RoleManager<IdentityRole> roleManager,
                                 SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user is null)
                return View(model);


            var roles = await _userManager.GetRolesAsync(user);
            if (roles is null || roles.Count() == 0)
                return View(model);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("Stamp", user.SecurityStamp.ToString())
            };

            IEnumerable<Claim> roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role));

            claims.AddRange(roleClaims);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTime.Now.AddDays(30),// Session duration
                IsPersistent = true,
                IssuedUtc = DateTime.Now,
            };

            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            try
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);

                return RedirectToAction("Index", returnUrl ?? "Home");
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);

                return RedirectToAction("Index", "Account");
            }
            }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Fullname = model.FullName,
                    UserName = model.UserName,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home"); 
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Account"); 
        }
    }
}

