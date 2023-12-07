using LMIS.Web.DTOs.Genre;
using LMIS.Web.Models;
using LMIS.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace LMIS.Web.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize]
    public class GenresController : Controller
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // GET: GenresController
        public async Task<ActionResult> Index()
        {
            string token = GetToken();

            // Fetch genres
            List<Genre> genresList = await _genreService.GetAllGenres(token);

            ViewBag.genresList = genresList;

            return View();
        }

        // GET: GenresController/Details/5
        public ActionResult Details(int id)
        {
            // Implementation for viewing genre details if needed
            return View();
        }

        // GET: GenresController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GenresController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(GenreDTO model)
        {
            if (ModelState.IsValid)
            {
                string token = this.GetToken();
                // Send data to API to create a genre
                bool result = await _genreService.CreateGenre(model, token);

                if (result)
                {
                    TempData["success_response"] = "Genre has been created successfully";
                }
                else
                {
                    TempData["error_response"] = "Failed to create genre";
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View("Index", model);
            }
        }

        // GET: GenresController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            // Fetch genre details for editing
            string token = GetToken();
            Genre genre = await _genreService.GetGenre(id, token);

            if (genre != null)
                return Json(genre);

            return Json(null);
        }

        // POST: GenresController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(GenreDTO model)
        {
            string token = GetToken();

            if (ModelState.IsValid)
            {
                
                // Send data to API to update a genre
                bool result = await _genreService.UpdateGenre(model, token);

                if (result)
                {
                    TempData["success_response"] = "Genre has been updated successfully";
                }
                else
                {
                    TempData["error_response"] = "Failed to update genre";
                }

                return RedirectToAction("Index");
            }
            else
            {
                // Fetch genres
                List<Genre> genresList = await _genreService.GetAllGenres(token);

                ViewBag.genresList = genresList;

                return View("Index", model);
            }
        }

        // GET: GenresController/Delete/5
        //[HttpGet]
        //public ActionResult Delete(int id)
        //{
        //    // Implementation for confirming genre deletion if needed
        //    return View();
        //}

        // POST: GenresController/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            string token = GetToken();

            // Send request to API to delete a genre
            bool result = await _genreService.DeleteGenre(id, token);

            if (result)
            {
                TempData["success_response"] = "Genre has been deleted successfully";
                return Json(new { status = "success" });
            }
            else
            {
                TempData["error_response"] = "Failed to delete genre";
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
