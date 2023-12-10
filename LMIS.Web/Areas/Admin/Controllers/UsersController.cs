using LMIS.Web.DTOs.User;
using LMIS.Web.Models;
using LMIS.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Common;

namespace LMIS.Web.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            this._userService = userService;
        }
        // GET: UsersController
        public async Task<ActionResult> Index()
        {
            string token = null;
            if (Request.Cookies["token"] != null)
            {
                token = Request.Cookies["token"];
            }
            //fetch users 

            List<User> usersList = await this._userService.GetAllUsers(token);

            ViewBag.usersList = usersList;

            await PopulateViewBags();

            return View();
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]

        public async Task<ActionResult> Create(UserDTO model)
        {
            if (ModelState.IsValid)
            {
                //fetch record from the API

                string token = this.GetToken();

                //send data to api

                bool result = await this._userService.CreateUser(model,token);

                if(result)
                {
                    TempData["success_response"] = "user has been created successfully";

                }
                else
                {
                    TempData["error_response"] = "failed to create user";

                }


                return RedirectToAction("Index");
            }
            else
            {
                return View("Index", model);
            }
           
        }

        // GET: UsersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            //fetch record from the API

            string token = this.GetToken();

            User user = await this._userService.GetUser(id,token);

            if(user != null)
                return Json(user);

            return Json(null);
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserDTO model)
        {
            if (ModelState.IsValid)
            {
                string token = GetToken();
                //send data to api

                bool result = await this._userService.UpdateUser(model,token);

                if (result)
                {
                    TempData["success_response"] = "user has been updated successfully";

                }
                else
                {
                    TempData["error_response"] = "failed to update user";

                }


                return RedirectToAction("Index");
            }
            else
            {
                return View("Index", model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {

                if (Request.Cookies["token"] != null)
                {
                    Response.Cookies.Delete("token");
                }

                HttpContext.Session.Remove("token");

                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home", new {Area =""});

            
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {

            string token = GetToken();

            bool result = await this._userService.DeleteUser(id, token);

            if (result == false)
                return Json(new { status = "failed" });



            return Json(new { status = "success" });
        }

        private async Task PopulateViewBags()
        {

            ViewBag.rolesList = await this.GetRoles();
            ViewBag.genderList = GetGenderList();
        }

        private async Task<List<SelectListItem>> GetRoles()
        {
            string token = GetToken();
            
            List<SelectListItem> roles = new List<SelectListItem>();

            var rolesList =  await this._userService.GetAllRoles(token);

            foreach (var item in rolesList)
            {
                roles.Add(new SelectListItem() { Text = item.RoleName, Value = item.RoleName });
            }

            return roles;
        }

        private List<SelectListItem> GetGenderList()
        {
            var genderList = new List<SelectListItem>();

            genderList.Add(new SelectListItem() { Text = "Male", Value = "Male" });
            genderList.Add(new SelectListItem() { Text = "Female", Value = "Female" });

            return genderList;
        }

        private string GetToken()
        {
            string token = string.Empty;
            if (!string.IsNullOrEmpty(Request.Cookies["token"]))
            {
                token = Request.Cookies["token"];
            }

            return token;
        }
    }
}
