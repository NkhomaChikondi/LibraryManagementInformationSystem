using LMIS.Web.DTOs.Role;
using LMIS.Web.Models;
using LMIS.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMIS.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class RolesController : Controller
    {
        private readonly IroleService _roleService;

        public RolesController(IroleService roleService)
        {
            _roleService = roleService;
        }

        // GET: RolesController
        public async Task<ActionResult> Index()
        {
            string token = GetToken();

            // Fetch roles
            List<Role> rolesList = await _roleService.GetAllRoles(token);

            ViewBag.rolesList = rolesList;

            return View();
        }

        // GET: RolesController/Details/5
        public ActionResult Details(int id)
        {
            // Implementation for viewing role details if needed
            return View();
        }

        // GET: RolesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleDTO model)
        {
            if (ModelState.IsValid)
            {
                string token = this.GetToken();
                // Send data to API to create a role
                bool result = await _roleService.CreateRole(model, token);

                if (result)
                {
                    TempData["success_response"] = "Role has been created successfully";
                }
                else
                {
                    TempData["error_response"] = "Failed to create role";
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View("Index", model);
            }
        }

        // GET: RolesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            // Fetch role details for editing
            string token = GetToken();
            Role role = await _roleService.GetRole(id, token);

            if (role != null)
                return Json(role);

            return Json(null);
        }

        // POST: RolesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RoleDTO model)
        {
            if (ModelState.IsValid)
            {
                string token = GetToken();

                // Send data to API to update a role
                bool result = await _roleService.UpdateRole(model, token);

                if (result)
                {
                    TempData["success_response"] = "Role has been updated successfully";
                }
                else
                {
                    TempData["error_response"] = "Failed to update role";
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View("Index", model);
            }
        }

        // GET: RolesController/Delete/5
        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    // Implementation for confirming role deletion if needed
        //    return View();
        //}

        // POST: RolesController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            string token = GetToken();

            // Send request to API to delete a role
            bool result = await _roleService.DeleteRole(id, token);

            if (result)
            {
                TempData["success_response"] = "Role has been deleted successfully";


                return Json(new { status = "success" });

            }
            else
            {
                TempData["error_response"] = "Failed to delete role";

                return Json(new { status = "failed" });
            }

           
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
