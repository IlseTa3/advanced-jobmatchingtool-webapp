using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using advanced_jobmatchingtool_webapp.Models;
using Microsoft.AspNetCore.Authorization;

namespace advanced_jobmatchingtool_webapp.Controllers.Beheer
{
    [Authorize (Roles = "Beheerder")]
    public class ProspectenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProspectenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prospecten
        public async Task<IActionResult> Index()
        {
            return View(await _context.Prospecten.ToListAsync());
        }

        // GET: Prospecten/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prospect = await _context.Prospecten
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prospect == null)
            {
                return NotFound();
            }

            return View(prospect);
        }

        // GET: Prospecten/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prospecten/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NaamOnderneming,NaamContactpersoon,Btwnr,Adres,Postcode,Stad,Land,Telefoonnr,Gsmnr,Email,OmschrijvingOnderneming,TermsCond")] Prospect prospect)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prospect);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prospect);
        }

        // GET: Prospecten/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prospect = await _context.Prospecten.FindAsync(id);
            if (prospect == null)
            {
                return NotFound();
            }
            return View(prospect);
        }

        // POST: Prospecten/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NaamOnderneming,NaamContactpersoon,Btwnr,Adres,Postcode,Stad,Land,Telefoonnr,Gsmnr,Email,OmschrijvingOnderneming,TermsCond")] Prospect prospect)
        {
            if (id != prospect.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prospect);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProspectExists(prospect.Id))
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
            return View(prospect);
        }

        // GET: Prospecten/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prospect = await _context.Prospecten
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prospect == null)
            {
                return NotFound();
            }

            return View(prospect);
        }

        // POST: Prospecten/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prospect = await _context.Prospecten.FindAsync(id);
            if (prospect != null)
            {
                _context.Prospecten.Remove(prospect);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProspectExists(int id)
        {
            return _context.Prospecten.Any(e => e.Id == id);
        }
    }
}
