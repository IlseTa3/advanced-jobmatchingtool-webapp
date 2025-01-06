using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace advanced_jobmatchingtool_webapp.Controllers
{

    [Authorize(Policy ="RequiredKandidaat")]
    public class KandidatenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IKandidaatService _kandidaatService;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<KandidatenController> _logger;

        public KandidatenController(ApplicationDbContext context, IKandidaatService kandidaatService, IEmailSender emailSender, ILogger<KandidatenController> logger)
        {
            _context = context;
            _kandidaatService = kandidaatService ?? throw new ArgumentNullException(nameof(kandidaatService));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
            _logger = logger;

            _logger.LogInformation("KandidatenController is geïnitialiseerd");
        }

        // GET: Kandidaten
        public async Task<IActionResult> Index()
        {
            
                var kandidaten = await _kandidaatService.GetAllKandidatenAsync();
                return View(kandidaten);
            
            
        }

        // GET: Kandidaten/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kandidaat = await _kandidaatService.GetKandidaatByIdAsync(id.Value);
            if (kandidaat == null)
            {
                return NotFound();
            }

            return View(kandidaat);
        }

        // GET: Kandidaten/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kandidaten/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Voornaam,Familienaam,Geboortedatum,Email,GsmNr,Straat,Huisnr,Busnr,Postcode,Stad")] Kandidaat kandidaat)
        {
            if (ModelState.IsValid)
            {
                await _kandidaatService.CreateKandidaatAsync(kandidaat);
                //E-mail versturen
                try
                {
                    string subject = "Bevestiging van registratie";
                    string message = $"Beste {kandidaat.Voornaam} {kandidaat.Familienaam}, <br><br> " +
                        "Hieronder een overzicht van jouw geregistreerde gegevens: <br/><br/> " +
                        $"Voornaam: {kandidaat.Voornaam}<br/><br/>" +
                        $"Familienaam: {kandidaat.Familienaam}<br/><br/>" +
                        $"Geboortedatum: {kandidaat.Geboortedatum}<br/><br/>" +
                        $"E-mail: {kandidaat.Email}<br/><br/>" +
                        $"Gsmnummer: {kandidaat.GsmNr}<br/><br/>" +
                        $"Straat: {kandidaat.Straat}<br/><br/>" +
                        $"Huisnummer: {kandidaat.Huisnr}<br/><br/>" +
                        $"Busnummer: {kandidaat.Busnr}<br/><br/>" +
                        $"Postcode: {kandidaat.Postcode}<br/><br/>" +
                        $"Stad/Gemeente: {kandidaat.Stad}<br/><br/>" +
                             "Een Krassemedewerker neemt zo spoedig mogelijk contact met je op.<br><br>" +
                             "Je kan alvast de vragenlijst invullen op het platform (indien je dit nog niet hebt gedaan) <br/><br/>" +
                             "Met vriendelijke groet,<br>Het KrasSwerk Team";
                    await _emailSender.SendEmailAsync(kandidaat.Email, subject, message);

                    //Bericht voor pop-up
                    TempData["EmailSuccess"] = "De bevestigingsmail werd succesvol verzonden!";
                }
                catch (Exception)
                {

                    TempData["EmailError"] = "Er is een fout opgetreden bij het verzenden van de e-mail.";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(kandidaat);
        }

        // GET: Kandidaten/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kandidaat = await _kandidaatService.GetKandidaatByIdAsync(id.Value);
            if (kandidaat == null)
            {
                return NotFound();
            }
            return View(kandidaat);
        }

        // POST: Kandidaten/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Voornaam,Familienaam,Geboortedatum,Email,GsmNr,Straat,Huisnr,Busnr,Postcode,Stad")] Kandidaat kandidaat)
        {
            if (id != kandidaat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _kandidaatService.UpdateKandidaatAsync(kandidaat);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KandidaatExists(kandidaat.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

            }
            return View(kandidaat);
        }

        // GET: Kandidaten/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kandidaat = await _kandidaatService.GetKandidaatByIdAsync(id.Value);
            if (kandidaat == null)
            {
                return NotFound();
            }

            return View(kandidaat);
        }

        // POST: Kandidaten/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _kandidaatService.DeleteKandidaatAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private bool KandidaatExists(int id)
        {
            return _context.Kandidaten.Any(e => e.Id == id);
        }
    }
}
