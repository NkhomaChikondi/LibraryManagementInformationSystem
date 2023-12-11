using LMIS.Web.DTOs.Member;
using LMIS.Web.Models;
using LMIS.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMIS.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class MembersController : Controller
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            this._memberService = memberService;
        }

        // GET: MembersController
        public async Task<ActionResult> Index()
        {
            // Implement logic to fetch members
            List<Member> membersList = await this._memberService.GetAllMembers(GetToken());

            ViewBag.membersList = membersList;

            await PopulateViewBags();

            return View();
        }

        // GET: MembersController/Details/5
        public ActionResult Details(int id)
        {
            // Implement logic to fetch member details by id
            return View();
        }

        // GET: MembersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MembersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(MemberDTO model)
        {
            if (ModelState.IsValid)
            {
                // Send data to API to create a member
                bool result = await this._memberService.CreateMember(model, GetToken());

                if (result)
                {
                    TempData["success_response"] = "Member has been created successfully";
                }
                else
                {
                    TempData["error_response"] = "Failed to create member";
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View("Index", model);
            }
        }

        // GET: MembersController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            // Fetch record from the API
            Member member = await this._memberService.GetMember(id, GetToken());

            if (member != null)
                return Json(member);

            return Json(null);
        }

        // POST: MembersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MemberDTO model)
        {
            if (ModelState.IsValid)
            {
                // Send data to API to update a member
                bool result = await this._memberService.UpdateMember(model, GetToken());

                if (result)
                {
                    TempData["success_response"] = "Member has been updated successfully";
                }
                else
                {
                    TempData["error_response"] = "Failed to update member";
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View("Index", model);
            }
        }

        // POST: MembersController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            bool result = await this._memberService.DeleteMember(id, GetToken());

            if (result == false)
                return Json(new { status = "failed" });

            return Json(new { status = "success" });
        }

        private async Task PopulateViewBags()
        {
            // Implement logic to populate ViewBag properties like rolesList and genderList
           // ViewBag.rolesList = await this.GetRoles();
            ViewBag.genderList = GetGenderList();
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
