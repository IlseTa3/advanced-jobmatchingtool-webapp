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
using advanced_jobmatchingtool_webapp.Services.Klant;

namespace advanced_jobmatchingtool_webapp.Controllers.Klant
{
    public class ProspectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IProspectService _prospectService;

        public ProspectsController(ApplicationDbContext context,IEmailService emailService,IProspectService prospectService)
        {
            _context = context;
            _emailService = emailService;
            _prospectService = prospectService;
        }

        // GET: Prospects
        [Authorize(Roles ="Beheerder")]
        public async Task<IActionResult> Index()
        {

            var model = await _prospectService.GetAllProspectsAsync();
            return View(model);
            
        }

        // GET: Prospects/Details/5
        [Authorize(Roles = "Beheerder")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prospect = await _prospectService.GetProspectByIdAsync(id.Value);
            if (prospect == null)
            {
                return NotFound();
            }

            return View(prospect);
        }

        // GET: Prospects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Prospects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NaamOnderneming,NaamContactpersoon,Btwnr,Adres,Postcode,Stad,Land,Telefoonnr,Gsmnr,Email,OmschrijvingOnderneming,TermsCond")] Prospect prospect)
        {
            if (ModelState.IsValid)
            {
                await _prospectService.CreateProspectAsync(prospect);

                await _emailService.SendEmailAsync(prospect.Email, "Bevestiging van contactaanvraag",
                        $"Beste {prospect.NaamContactpersoon}, <br/><br/> Bedankt voor je interesse in Opus Aptus. We nemen zo spoedig mogelijk contact met je op." +
                            "<br/><br/> Met vriendelijke groet, <br/> Het Opus Aptus Team"
                    );

                return RedirectToAction(nameof(Bevestiging));
            }
            return View(prospect);
        }

        //GET: Prospects/Bevestiging
        public IActionResult Bevestiging() { return View(); }

        // GET: Prospects/Edit/5
        [Authorize(Roles = "Beheerder")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prospect = await _prospectService.GetProspectByIdAsync(id.Value);
            if (prospect == null)
            {
                return NotFound();
            }
            return View(prospect);
        }

        // POST: Prospects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Beheerder")]
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
                    await _prospectService.UpdateProspectAsync(prospect);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_prospectService.ProspectExists(prospect.Id))
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

        // GET: Prospects/Delete/5
        [Authorize(Roles = "Beheerder")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prospect = await _prospectService.GetProspectByIdAsync(id.Value);
            if (prospect == null)
            {
                return NotFound();
            }

            return View(prospect);
        }

        // POST: Prospects/Delete/5
        [Authorize(Roles = "Beheerder")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _prospectService.DeleteProspectByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProspectExists(int id)
        {
            return _context.Prospecten.Any(e => e.Id == id);
        }
    }
}
