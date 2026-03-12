using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using advanced_jobmatchingtool_webapp.Models;
using Microsoft.AspNetCore.Authorization;
using advanced_jobmatchingtool_webapp.Services.Kandidaat;

namespace advanced_jobmatchingtool_webapp.Controllers.Beheer
{
    [Authorize(Roles = "Beheerder")]
    public class VragenKandidaatController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IVraagKandidaatService _vraagKandidaatService;

        public VragenKandidaatController(ApplicationDbContext context, IVraagKandidaatService vraagKandidaatService)
        {
            _context = context;
            _vraagKandidaatService = vraagKandidaatService;
        }

        // GET: VragenKandidaat
        public async Task<IActionResult> Index()
        {
            var model = await _vraagKandidaatService.GetAllVragenAsync();
            return View(model);
        }

        // GET: VragenKandidaat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vraag = await _vraagKandidaatService.GetVraagCatSubOptiesByIdAsync(id.Value);
            if (vraag == null)
            {
                return NotFound();
            }

            return View(vraag);
        }

        // GET: VragenKandidaat/Create
        public async Task<IActionResult> Create()
        {

            var catSubcat = from cat in _context.CategorieSubCats
                            select new { cat.Id, CategorieSubCategorie = cat.NaamCategorie + " - " + cat.NaamSubCategorie };

            ViewData["AntwoordOptieId"] = new SelectList(await _vraagKandidaatService.GetAllAntwoordOptiesAsync(), "Id", "OptieTekst");
            ViewData["CategorieSubCatId"] = new SelectList(catSubcat, "Id", "CategorieSubCategorie");
            ViewData["SoortAntwoord"] = new SelectList(Enum.GetValues(typeof(EnumSoortAntwoord)));
            return View();
        }

        // POST: VragenKandidaat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VraagText,CategorieSubCatId,AntwoordOptieId,SoortAntwoord")] VraagKandidaat vraag)
        {
            if (ModelState.IsValid)
            {
                await _vraagKandidaatService.CreateVraagAsync(vraag);
                return RedirectToAction(nameof(Index));
            }

            var catSubcat = from cat in _context.CategorieSubCats
                            select new { cat.Id, CategorieSubCategorie = cat.NaamCategorie + " - " + cat.NaamSubCategorie };

            ViewData["AntwoordOptieId"] = new SelectList(await _vraagKandidaatService.GetAllAntwoordOptiesAsync(), "Id", "OptieTekst", vraag.AntwoordOptieId);
            ViewData["CategorieSubCatId"] = new SelectList(catSubcat, "Id", "CategorieSubCategorie", vraag.CategorieSubCatId);
            ViewData["SoortAntwoord"] = new SelectList(Enum.GetValues(typeof(EnumSoortAntwoord)), vraag.SoortAntwoord);
            return View(vraag);
        }

        // GET: VragenKandidaat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vraag = await _vraagKandidaatService.GetVraagByIdAsync(id.Value);
            if (vraag == null)
            {
                return NotFound();
            }

            var catSubcat = from cat in _context.CategorieSubCats
                            select new { cat.Id, CategorieSubCategorie = cat.NaamCategorie + " - " + cat.NaamSubCategorie };

            ViewData["AntwoordOptieId"] = new SelectList(await _vraagKandidaatService.GetAllAntwoordOptiesAsync(), "Id", "OptieTekst", vraag.AntwoordOptieId);
            ViewData["CategorieSubCatId"] = new SelectList(catSubcat, "Id", "CategorieSubCategorie", vraag.CategorieSubCatId);
            ViewData["SoortAntwoord"] = new SelectList(Enum.GetValues(typeof(EnumSoortAntwoord)), vraag.SoortAntwoord);

            return View(vraag);
        }

        // POST: VragenKandidaat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VraagText,CategorieSubCatId,AntwoordOptieId,SoortAntwoord")] VraagKandidaat vraag)
        {
            if (id != vraag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _vraagKandidaatService.UpdateVraagAsync(vraag);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_vraagKandidaatService.VraagExists(vraag.Id))
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

            var catSubcat = from cat in _context.CategorieSubCats
                            select new { cat.Id, CategorieSubCategorie = cat.NaamCategorie + " - " + cat.NaamSubCategorie };

            ViewData["AntwoordOptieId"] = new SelectList(await _vraagKandidaatService.GetAllAntwoordOptiesAsync(), "Id", "OptieTekst", vraag.AntwoordOptieId);
            ViewData["CategorieSubCatId"] = new SelectList(catSubcat, "Id", "CategorieSubCategorie", vraag.CategorieSubCatId);
            ViewData["SoortAntwoord"] = new SelectList(Enum.GetValues(typeof(EnumSoortAntwoord)), vraag.SoortAntwoord);
            return View(vraag);
        }

        // GET: VragenKandidaat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vraag = await _vraagKandidaatService.GetVraagCatSubOptiesByIdAsync(id.Value);
            if (vraag == null)
            {
                return NotFound();
            }

            return View(vraag);
        }

        // POST: VragenKandidaat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vraagKandidaatService.DeleteVraagAsync(id);
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> GetAllVragenKandidatenData()
        {
            var data = await _vraagKandidaatService.GetAllVragenAsync();
            return Json(new { data });
        }

        private bool VraagExists(int id)
        {
            return _context.VragenKandidaten.Any(e => e.Id == id);
        }
    }
}
