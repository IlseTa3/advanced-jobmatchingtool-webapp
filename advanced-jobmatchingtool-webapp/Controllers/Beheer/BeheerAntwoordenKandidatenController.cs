using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Services.Beheer;
using Microsoft.AspNetCore.Authorization;

namespace advanced_jobmatchingtool_webapp.Controllers.Beheer
{
    [Authorize(Roles = "Beheerder")]
    public class BeheerAntwoordenKandidatenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBeheerAntwoordKandidaatService _beheerAntwoordKandidaatService;

        public BeheerAntwoordenKandidatenController(ApplicationDbContext context, IBeheerAntwoordKandidaatService beheerAntwoordKandidaatService)
        {
            _context = context;
            _beheerAntwoordKandidaatService = beheerAntwoordKandidaatService;
        }

        // GET: BeheerAntwoordenKandidaten
        public async Task<IActionResult> Index()
        {
            
            var model = await _beheerAntwoordKandidaatService.GetAllAntwoordenKandidaatAsync();
            return View(model);
        }


        // GET: BeheerAntwoordenKandidaten/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var antwoordKandidaat = await _beheerAntwoordKandidaatService.GetAntwoordKandidaatByIdAsync(id.Value);
            if (antwoordKandidaat == null)
            {
                return NotFound();
            }
            

            ViewData["UserNaam"] = antwoordKandidaat.User != null
                ? $"{antwoordKandidaat.User.Voornaam}  {antwoordKandidaat.User.Familienaam}" : "Onbekende gebruiker";
            ViewData["VraagTekst"] = antwoordKandidaat.VraagKandidaat != null
                ? antwoordKandidaat.VraagKandidaat.VraagText : "Onbekende vraag";
            return View(antwoordKandidaat);
        }

        // POST: BeheerAntwoordenKandidaten/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VraagKandidaatId,UserId,AntwoordTekst,ExtraInfo,Categorie,DatumIngevuld")] AntwoordKandidaat antwoordKandidaat)
        {
            if (id != antwoordKandidaat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _beheerAntwoordKandidaatService.UpdateAntwoordKandidaatAsync(antwoordKandidaat);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_beheerAntwoordKandidaatService.AntwoordExists(antwoordKandidaat.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", antwoordKandidaat.UserId);
            ViewData["VraagKandidaatId"] = new SelectList(_context.VragenKandidaten, "Id", "VraagText", antwoordKandidaat.VraagKandidaatId);
            return View(antwoordKandidaat);
        }

        public async Task<IActionResult> GetAllAntwoordenKandidatenData()
        {
            var data = await _beheerAntwoordKandidaatService.GetAllAntwoordenKandidaatAsync();
            return Json(new { data });
        }

        private bool AntwoordKandidaatExists(int id)
        {
            return _context.AntwoordenKandidaten.Any(e => e.Id == id);
        }
    }
}
