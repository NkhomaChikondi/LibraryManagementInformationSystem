using LMIS.Web.DTOs.User;
using LMIS.Web.Models;
using LMIS.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserDTO model)
        {
            if (ModelState.IsValid)
            {
               
                //send data to api

                await this._userService.CreateUser(model);

                return RedirectToAction("Index");
            }
            else
            {
                return View("Index", model);
            }
           
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

      

        // POST: UsersController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {

            string token = GetToken();

            bool result = await this._userService.DeleteUser(id, token);

            if (result == false)
                return Json(new { status = "failed" });



            return Json(new { status = "success"});
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
