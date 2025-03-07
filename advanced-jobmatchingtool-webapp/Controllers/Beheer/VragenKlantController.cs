using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using advanced_jobmatchingtool_webapp.Models;
using Microsoft.AspNetCore.Authorization;
using advanced_jobmatchingtool_webapp.Services.Klant;

namespace advanced_jobmatchingtool_webapp.Controllers.Beheer
{
    [Authorize(Roles = "Beheerder")]
    public class VragenKlantController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IVraagKlantService _vraagKlantService;

        public VragenKlantController(ApplicationDbContext context, IVraagKlantService vraagKlantService)
        {
            _context = context;
            _vraagKlantService = vraagKlantService;
        }

        // GET: VragenKlant
        public async Task<IActionResult> Index()
        {
            var model = await _vraagKlantService.GetAllVragenAsync();
            return View(model);
        }

        // GET: VragenKlant/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vraag = await _vraagKlantService.GetVraagCatSubOptiesByIdAsync(id.Value);
            if (vraag == null)
            {
                return NotFound();
            }

            return View(vraag);
        }

        // GET: VragenKlant/Create
        public async Task<IActionResult> Create()
        {
            var catSubcat = from cat in _context.CategorieSubCats
                            select new { cat.Id, CategorieSubCategorie = cat.NaamCategorie + " - " + cat.NaamSubCategorie };

            ViewData["AntwoordOptieId"] = new SelectList(await _vraagKlantService.GetAllAntwoordOptiesAsync(), "Id", "OptieTekst");
            ViewData["CategorieSubCatId"] = new SelectList(catSubcat, "Id", "CategorieSubCategorie");
            ViewData["SoortAntwoord"] = new SelectList(Enum.GetValues(typeof(EnumSoortAntwoord)));
            return View();
        }

        // POST: VragenKlant/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VraagText,CategorieSubCatId,AntwoordOptieId,SoortAntwoord")] VraagKlant vraag)
        {
            if (ModelState.IsValid)
            {
                await _vraagKlantService.CreateVraagAsync(vraag);
                return RedirectToAction(nameof(Index));
            }

            var catSubcat = from cat in _context.CategorieSubCats
                            select new { cat.Id, CategorieSubCategorie = cat.NaamCategorie + " - " + cat.NaamSubCategorie };

            ViewData["AntwoordOptieId"] = new SelectList(await _vraagKlantService.GetAllAntwoordOptiesAsync(), "Id", "OptieTekst", vraag.AntwoordOptieId);
            ViewData["CategorieSubCatId"] = new SelectList(catSubcat, "Id", "CategorieSubCategorie", vraag.CategorieSubCatId);
            ViewData["SoortAntwoord"] = new SelectList(Enum.GetValues(typeof(EnumSoortAntwoord)), vraag.SoortAntwoord);
            return View(vraag);
        }

        // GET: VragenKlant/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vraag = await _vraagKlantService.GetVraagByIdAsync(id.Value);
            if (vraag == null)
            {
                return NotFound();
            }


            var catSubcat = from cat in _context.CategorieSubCats
                            select new { cat.Id, CategorieSubCategorie = cat.NaamCategorie + " - " + cat.NaamSubCategorie };

            ViewData["AntwoordOptieId"] = new SelectList(await _vraagKlantService.GetAllAntwoordOptiesAsync(), "Id", "OptieTekst", vraag.AntwoordOptieId);
            ViewData["CategorieSubCatId"] = new SelectList(catSubcat, "Id", "CategorieSubCategorie", vraag.CategorieSubCatId);
            ViewData["SoortAntwoord"] = new SelectList(Enum.GetValues(typeof(EnumSoortAntwoord)), vraag.SoortAntwoord);
            return View(vraag);
        }

        // POST: VragenKlant/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VraagText,CategorieSubCatId,AntwoordOptieId,SoortAntwoord")] VraagKlant vraag)
        {
            if (id != vraag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _vraagKlantService.UpdateVraagAsync(vraag);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_vraagKlantService.VraagExists(vraag.Id))
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

            ViewData["AntwoordOptieId"] = new SelectList(await _vraagKlantService.GetAllAntwoordOptiesAsync(), "Id", "OptieTekst", vraag.AntwoordOptieId);
            ViewData["CategorieSubCatId"] = new SelectList(catSubcat, "Id", "CategorieSubCategorie", vraag.CategorieSubCatId);
            ViewData["SoortAntwoord"] = new SelectList(Enum.GetValues(typeof(EnumSoortAntwoord)), vraag.SoortAntwoord);
            return View(vraag);
        }

        // GET: VragenKlant/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vraag = await _vraagKlantService.GetVraagCatSubOptiesByIdAsync(id.Value);
            if (vraag == null)
            {
                return NotFound();
            }

            return View(vraag);
        }

        // POST: VragenKlant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _vraagKlantService.DeleteVraagAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetAllVragenKlantenData()
        {
            var data = await _vraagKlantService.GetAllVragenAsync();
            return Json(new { data });
        }

        private bool VraagExists(int id)
        {
            return _context.VragenKlanten.Any(e => e.Id == id);
        }
    }
}
