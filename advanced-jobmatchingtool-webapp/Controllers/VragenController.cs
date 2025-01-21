using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace advanced_jobmatchingtool_webapp.Controllers
{
    [Authorize(Roles = "Beheerder")]
    public class VragenController : Controller
    {
        private readonly MongoDbService _dbService;
        private readonly ILogger<VragenController> _logger;
        public VragenController(MongoDbService dbService, ILogger<VragenController> logger)
        {
            _dbService = dbService;
            _logger = logger;
        }


        //Ophalen van data uit de MongoDb database.
        public async Task<IActionResult> Index()
        {
            var vragen = await _dbService.GetAlleVragenAsync();

            return View(vragen);
        }




        //GET: Vragen/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: Vragen/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vraag vraag)
        {
            if (!ModelState.IsValid)
            {
                return View(vraag);
            }

            await _dbService.AddVraagAsync(vraag);
            _logger.LogInformation("Nieuwe vraag toegevoegd: " + vraag.VraagText);
            return RedirectToAction(nameof(Index));
        }

        //GET Vragen/Edit/1
        public async Task<IActionResult> Edit(string id)
        {
            var vraag = await _dbService.GetVraagByIdAsync(id);
            if (vraag == null)
            {
                return NotFound();
            }

            return View(vraag);
        }

        //POST Vragen/Edit/1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Vraag updatedVraag)
        {
            if (id != updatedVraag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _dbService.UpdateVraagAsync(updatedVraag);
                return RedirectToAction(nameof(Index));
            }

            return View(updatedVraag);
        }

        //GET Vragen/Delete/1
        public async Task<IActionResult> Delete(string id)
        {
            var vraag = await _dbService.GetVraagByIdAsync(id);
            if (vraag == null)
            {
                return NotFound();
            }

            return View(vraag);
        }

        //POST Vragen/Delete/1

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _dbService.DeleteVraagAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //Alle vragen Ophalen
        public async Task<IActionResult> LoadAllVragen()
        {
            try
            {
                // Ophalen van gegevens uit de database
                var vragenData = await _dbService.GetAlleVragenAsync();

                // Maak de lijst van opties als een string, indien gewenst
                var vragenWithOptiesString = vragenData.Select(v => new
                {
                    v.Id,
                    v.VraagText,
                    v.Categorie,
                    v.SubCategorie,
                    v.Type,
                    v.Opties,
                    v.ExtraInformatie,
                    v.VoorWie
                }).ToList();

                return Json(new { data = vragenWithOptiesString });
            }
            catch (Exception ex)
            {
                // Foutafhandelingslogica
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
