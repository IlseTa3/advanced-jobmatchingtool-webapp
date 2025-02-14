using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Services;

namespace advanced_jobmatchingtool_webapp.Controllers
{
    public class SubCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISubCategorieService _subCategorieService;

        public SubCategoriesController(ApplicationDbContext context, ISubCategorieService subCategorieService)
        {
            _context = context;
            _subCategorieService = subCategorieService;
        }

        // GET: SubCategories
        public async Task<IActionResult> Index()
        {
            var subCategorieen = await _subCategorieService.GetAllSubCategoriesAsync();
            return View(subCategorieen);
        }

        // GET: SubCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategorie = await _subCategorieService.GetSubCategorieByIdAsync(id.Value);
            if (subCategorie == null)
            {
                return NotFound();
            }

            return View(subCategorie);
        }

        // GET: SubCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NaamSubCategorie")] SubCategorie subCategorie)
        {
            if (ModelState.IsValid)
            {
                await _subCategorieService.CreateSubCategorieAsync(subCategorie);
                return RedirectToAction(nameof(Index));
            }
            return View(subCategorie);
        }

        // GET: SubCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategorie = await _subCategorieService.GetSubCategorieByIdAsync(id.Value);
            if (subCategorie == null)
            {
                return NotFound();
            }
            return View(subCategorie);
        }

        // POST: SubCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NaamSubCategorie")] SubCategorie subCategorie)
        {
            if (id != subCategorie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _subCategorieService.UpdateSubCategorieAsync(subCategorie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubCategorieExists(subCategorie.Id))
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
            return View(subCategorie);
        }

        // GET: SubCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subCategorie = await _subCategorieService.GetSubCategorieByIdAsync(id.Value);
            if (subCategorie == null)
            {
                return NotFound();
            }

            return View(subCategorie);
        }

        // POST: SubCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _subCategorieService.DeleteSubCategorieAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool SubCategorieExists(int id)
        {
            return _context.SubCategorieLijst.Any(e => e.Id == id);
        }
    }
}
