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
    public class AntwoordOptiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAntwoordOptieService _antwoordOptieService;

        public AntwoordOptiesController(ApplicationDbContext context, IAntwoordOptieService antwoordOptieService)
        {
            _context = context;
            _antwoordOptieService = antwoordOptieService;
        }

        // GET: AntwoordOpties
        public async Task<IActionResult> Index()
        {
            var antwoordOpties = await _antwoordOptieService.GetAllAntwoordOptiesAsync();
            return View(antwoordOpties);
        }

        // GET: AntwoordOpties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var antwoordOptie = await _antwoordOptieService.GetAntwoordOptieByIdAsync(id.Value);
            if (antwoordOptie == null)
            {
                return NotFound();
            }

            return View(antwoordOptie);
        }

        // GET: AntwoordOpties/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AntwoordOpties/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OptieTekst")] AntwoordOptie antwoordOptie)
        {
            if (ModelState.IsValid)
            {
                await _antwoordOptieService.CreateAntwoordOptieAsync(antwoordOptie);
                return RedirectToAction(nameof(Index));
            }
            return View(antwoordOptie);
        }

        // GET: AntwoordOpties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var antwoordOptie = await _antwoordOptieService.GetAntwoordOptieByIdAsync(id.Value);
            if (antwoordOptie == null)
            {
                return NotFound();
            }
            return View(antwoordOptie);
        }

        // POST: AntwoordOpties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OptieTekst")] AntwoordOptie antwoordOptie)
        {
            if (id != antwoordOptie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _antwoordOptieService.UpdateAntwoordOptieAsync(antwoordOptie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AntwoordOptieExists(antwoordOptie.Id))
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
            return View(antwoordOptie);
        }

        // GET: AntwoordOpties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var antwoordOptie = await _antwoordOptieService.GetAntwoordOptieByIdAsync(id.Value);
            if (antwoordOptie == null)
            {
                return NotFound();
            }

            return View(antwoordOptie);
        }

        // POST: AntwoordOpties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _antwoordOptieService.DeleteAntwoordOptieAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetAllAntwoordOptiesData()
        {
            var data = await _antwoordOptieService.GetAllAntwoordOptiesAsync();
            return Json(new { data });
        }

        private bool AntwoordOptieExists(int id)
        {
            return _context.AntwoordOpties.Any(e => e.Id == id);
        }
    }
}
