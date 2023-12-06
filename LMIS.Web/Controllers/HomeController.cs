using LMIS.Web.Models;
using LMIS.Web.Services.Interfaces;
using LMIS.Web.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Security.Claims;

namespace LMIS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private  Services.Interfaces.IAuthenticationService _authenticationService;

        public HomeController(ILogger<HomeController> logger, Services.Interfaces.IAuthenticationService authenticationService)
        {
            _logger = logger;
            _authenticationService = authenticationService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //validate model fields
            if(ModelState.IsValid)
            {
                //invoke authentication service

                AuthResult loginResult = await _authenticationService.Login(model.Email,model.Password);

                //check for errors from the login result

                if(loginResult.Status.Equals("failed",StringComparison.CurrentCultureIgnoreCase))
                {
                    ModelState.AddModelError("", loginResult.Message);
                    //return error to the user

                    return View("Index", model);


                }
                else
                {
                    //set cookies

                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Append("token", loginResult.User.Token, options);
                    Response.Cookies.Append("RoleName", loginResult.User.RoleName, options);
                    Response.Cookies.Append("FirstName", loginResult.User.FirstName, options);
                    Response.Cookies.Append("LastName", loginResult.User.LastName, options);

                    HttpContext.Session.SetString("token", loginResult.User.Token);


                    var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier, Convert.ToString(loginResult.User.UserId)),
                            new Claim(ClaimTypes.Name, Convert.ToString(model.Email)),
                            new Claim(ClaimTypes.Role, Convert.ToString(loginResult.User.RoleName)),

                        };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                        new AuthenticationProperties()
                        {
                            IsPersistent = true
                        });


                    // Set the ClaimsPrincipal as the current user
                    HttpContext.User = principal;


                }


            }
            else
            {
                return View("index", model);
            }

            return View("index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
