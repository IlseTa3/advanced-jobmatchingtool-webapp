using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Services;
using advanced_jobmatchingtool_webapp.ViewModels.Studies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace advanced_jobmatchingtool_webapp.Controllers
{
    [Authorize(Roles = "Kandidaat")]
    public class StudieVragenController : Controller
    {
        private readonly MongoDbVragenPerCategorieService _vragenPerCategorieService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<StudieVragenController> _logger;

        public StudieVragenController(MongoDbVragenPerCategorieService vragenPerCategorieService, UserManager<ApplicationUser> userManager, ApplicationDbContext context, ILogger<StudieVragenController> logger)
        {
            _vragenPerCategorieService = vragenPerCategorieService;
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var vragen = await _vragenPerCategorieService.GetVragenByCategorieAsync("Studies");

            var antwoorden = await _context.StudiesKandidaten
                .Where(a => a.UserId == userId && a.Categorie == "Studies")
                .ToListAsync();

            var model = new StudiesViewModel
            {
                UserId = userId,
                Voornaam = user.Voornaam,
                Familienaam = user.Familienaam,
                VragenAntwoorden = vragen.Select(v => new VraagAntwoordStudiesViewModel
                {
                    VraagId = v.Id,
                    VraagText = v.VraagText,
                    Type = v.Type,
                    Opties = v.Opties,
                    ExtraInformatie = v.ExtraInformatie,
                    Antwoord = antwoorden.FirstOrDefault(a => a.VraagId == v.Id)?.Antwoord,
                    Antwoorden = antwoorden.FirstOrDefault(a => a.VraagId == v.Id)?.Antwoord?.Split(", ").ToList()
                }).ToList(),
            };


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAntwoorden(List<VraagAntwoordStudiesViewModel> vragenAntwoorden)
        {
            var userId = _userManager.GetUserId(User);

            foreach (var item in vragenAntwoorden)
            {
                if (item.UserId != userId)
                {
                    _logger.LogWarning("Unauthorized attempt to save answers by user {UserId}", userId);
                    return Unauthorized();
                }

                // Log de waarden van de antwoorden
                _logger.LogInformation("Saving answer for VraagId: {VraagId}, Antwoord: {Antwoord}, Antwoorden: {Antwoorden}",
                    item.VraagId, item.Antwoord, string.Join(", ", item.Antwoorden ?? new List<string>()));

                // Controleer of het antwoord niet null of leeg is
                if (string.IsNullOrEmpty(item.Antwoord) && (item.Antwoorden == null || !item.Antwoorden.Any()))
                {
                    _logger.LogInformation("Skipping empty answer for VraagId: {VraagId}", item.VraagId);
                    continue; // Sla lege antwoorden over
                }

                // Stel de Antwoord-eigenschap in voor checkboxen
                if (item.Type == "Checkbox" && item.Antwoorden != null)
                {
                    item.Antwoord = string.Join(", ", item.Antwoorden);
                }

                // Opslaan in MySQL
                var bestaandAntwoord = await _context.StudiesKandidaten
                    .FirstOrDefaultAsync(a => a.UserId == userId && a.VraagId == item.VraagId);

                if (bestaandAntwoord != null)
                {
                    bestaandAntwoord.Antwoord = item.Antwoord;
                    _logger.LogInformation("Updated existing answer for VraagId: {VraagId} with value: {Antwoord}", item.VraagId, bestaandAntwoord.Antwoord);
                }
                else
                {
                    // Nieuw antwoord toevoegen
                    var nieuwAntwoord = new StudiesKandidaat
                    {
                        UserId = userId,
                        VraagId = item.VraagId,
                        Antwoord = item.Antwoord,
                        Categorie = "Studies",
                        DatumIngevuld = DateTime.Now,
                    };

                    _context.StudiesKandidaten.Add(nieuwAntwoord);
                    _logger.LogInformation("Added new answer for VraagId: {VraagId} with value: {Antwoord}", item.VraagId, nieuwAntwoord.Antwoord);
                }
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("Answers saved successfully for user {UserId}", userId);
            return RedirectToAction("Index", "StudieVragen");
        }
    }
}
