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
        private readonly EmailService _emailService;
        private readonly IProspectService _prospectService;

        public ProspectsController(ApplicationDbContext context,EmailService emailService,IProspectService prospectService)
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

                var naam = prospect.NaamContactpersoon;
                var email = prospect.Email;
                var onderwerp = "Bevestiging van contactaanvraag";
                var bericht = $@"
                                Beste {prospect.NaamContactpersoon},<br/><br/>
                                Bedankt voor je contactaanvraag bij Opus Aptus. Hieronder vind je een overzicht van de gegevens die je hebt ingevuld:<br/><br/>

                                <b>Naam onderneming:</b> {prospect.NaamOnderneming}<br/>
                                <b>Contactpersoon:</b> {prospect.NaamContactpersoon}<br/>
                                <b>BTW-nummer:</b> {prospect.Btwnr}<br/>
                                <b>Adres:</b> {prospect.Adres}, {prospect.Postcode} {prospect.Stad}, {prospect.Land}<br/>
                                <b>Telefoonnummer:</b> {prospect.Telefoonnr}<br/>
                                <b>GSM-nummer:</b> {prospect.Gsmnr}<br/>
                                <b>Email:</b> {prospect.Email}<br/>
                                <b>Omschrijving onderneming:</b><br/>
                                {prospect.OmschrijvingOnderneming}<br/><br/>
                                Indien er iets niet klopt, mag je ons gerust contacteren via dit e-mailadres.<br/><br/>
                                Met vriendelijke groet,<br/>
                                Het Opus Aptus Team
                                ";


                await _emailService.SendEmailAsync(naam, email, onderwerp, bericht);


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
