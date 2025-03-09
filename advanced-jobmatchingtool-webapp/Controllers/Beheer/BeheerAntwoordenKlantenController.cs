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
    public class BeheerAntwoordenKlantenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBeheerAntwoordKlantService _beheerAntwoordKlantService;

        public BeheerAntwoordenKlantenController(ApplicationDbContext context, IBeheerAntwoordKlantService antwoordKlantService)
        {
            _context = context;
            _beheerAntwoordKlantService = antwoordKlantService;
        }

        // GET: BeheerAntwoordenKlanten
        public async Task<IActionResult> Index()
        {
            var model = await _beheerAntwoordKlantService.GetAllAntwoordenKlantAsync();
            return View(model);
        }

        

        // GET: BeheerAntwoordenKlanten/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var antwoordKlant = await _beheerAntwoordKlantService.GetAntwoordKlantByIdAsync(id.Value);
            if (antwoordKlant == null)
            {
                return NotFound();
            }
            
            ViewData["UserNaam"] = antwoordKlant.User != null
                ? $"{antwoordKlant.User.Voornaam}  {antwoordKlant.User.Familienaam}" : "Onbekende gebruiker";
            ViewData["VraagTekst"] = antwoordKlant.VraagKlant != null
                ? antwoordKlant.VraagKlant.VraagText : "Onbekende vraag";
            return View(antwoordKlant);
        }

        // POST: BeheerAntwoordenKlanten/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VraagKlantId,UserId,AntwoordTekst,ExtraInfo,Categorie,DatumIngevuld")] AntwoordKlant antwoordKlant)
        {
            if (id != antwoordKlant.Id)
            {
                return NotFound();
            }

            

            if (ModelState.IsValid)
            {
                try
                {
                    await _beheerAntwoordKlantService.UpdateAntwoordKlantAsync(antwoordKlant);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_beheerAntwoordKlantService.AntwoordExists(antwoordKlant.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", antwoordKlant.UserId);
            ViewData["VraagKlantId"] = new SelectList(_context.VragenKlanten, "Id", "VraagText", antwoordKlant.VraagKlantId);
            return View(antwoordKlant);
        }

        public async Task<IActionResult> GetAllAntwoordenKlantenData()
        {
            var data = await _beheerAntwoordKlantService.GetAllAntwoordenKlantAsync();
            return Json(new { data });
        }

        private bool AntwoordKlantExists(int id)
        {
            return _context.AntwoordenKlanten.Any(e => e.Id == id);
        }
    }
}
