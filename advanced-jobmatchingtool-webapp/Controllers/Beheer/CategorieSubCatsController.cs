using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using advanced_jobmatchingtool_webapp.Models;
using Microsoft.AspNetCore.Authorization;
using advanced_jobmatchingtool_webapp.Services.Beheer;

namespace advanced_jobmatchingtool_webapp.Controllers.Beheer
{
    [Authorize(Roles = "Beheerder")]
    public class CategorieSubCatsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategorieSubCatService _catSubCatService;

        public CategorieSubCatsController(ApplicationDbContext context, ICategorieSubCatService catSubCatService)
        {
            _context = context;
            _catSubCatService = catSubCatService;
        }

        // GET: CategorieSubCats
        public async Task<IActionResult> Index()
        {
            var model = await _catSubCatService.GetAllCategoriesAsync();
            return View(model);
        }

        // GET: CategorieSubCats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorieSubCat = await _catSubCatService.GetCategorieByIdAsync(id.Value);
            if (categorieSubCat == null)
            {
                return NotFound();
            }

            return View(categorieSubCat);
        }

        // GET: CategorieSubCats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategorieSubCats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NaamCategorie,NaamSubCategorie")] CategorieSubCat categorieSubCat)
        {
            if (ModelState.IsValid)
            {
                await _catSubCatService.CreateCategorieAsync(categorieSubCat);
                return RedirectToAction(nameof(Index));
            }
            return View(categorieSubCat);
        }

        // GET: CategorieSubCats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorieSubCat = await _catSubCatService.GetCategorieByIdAsync(id.Value);
            if (categorieSubCat == null)
            {
                return NotFound();
            }
            return View(categorieSubCat);
        }

        // POST: CategorieSubCats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NaamCategorie,NaamSubCategorie")] CategorieSubCat categorieSubCat)
        {
            if (id != categorieSubCat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _catSubCatService.UpdateCategorieAsync(categorieSubCat);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategorieSubCatExists(categorieSubCat.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categorieSubCat);
        }

        // GET: CategorieSubCats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categorieSubCat = await _catSubCatService.GetCategorieByIdAsync(id.Value);
            if (categorieSubCat == null)
            {
                return NotFound();
            }

            return View(categorieSubCat);
        }

        // POST: CategorieSubCats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _catSubCatService.DeleteCategorieAsync(id);
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> GetCatSubCatData()
        {
            var data = await _catSubCatService.GetAllCategoriesAsync();
            return Json(new { data });
        }

        private bool CategorieSubCatExists(int id)
        {
            return _context.CategorieSubCats.Any(e => e.Id == id);
        }
    }
}
