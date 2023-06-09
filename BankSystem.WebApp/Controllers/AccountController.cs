using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BankSystem.WebApp.Services.Interfaces;

namespace BankSystem.WebApp.Controllers
{
    public class AccountController : Controller
    {

        private readonly IUserHttpService _userHttpService;

        public AccountController(IUserHttpService userHttpService)
        {
            _userHttpService = userHttpService;
        }
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(string username, string passcode)
        {

            try
            {
                var response = await _userHttpService.AuthenticateUser(username, passcode);
                if(response.IsSuccess)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, response.Data.UserName),
                        new Claim(ClaimTypes.PrimarySid, response.Data.UserId.ToString()),
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = true,
                        IsPersistent = true
             
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);
                    return RedirectToAction("Index", "Home");   
                }
            }
            catch (Exception ex)
            {

            }
        
            return View();
        }
    }
}
