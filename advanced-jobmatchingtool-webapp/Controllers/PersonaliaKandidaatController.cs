using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Services;
using advanced_jobmatchingtool_webapp.ViewModels.Antwoorden;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace advanced_jobmatchingtool_webapp.Controllers
{
    [Authorize(Roles = "Kandidaat")]
    public class PersonaliaKandidaatController : Controller
    {
        private readonly IAntwoordKandidaatService _antwoordKandidaatService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PersonaliaKandidaatController> _logger;
     
        public PersonaliaKandidaatController(IAntwoordKandidaatService antwoordKandidaatService, UserManager<ApplicationUser> userManager,
            ApplicationDbContext context, ILogger<PersonaliaKandidaatController> logger)
        {
            _antwoordKandidaatService = antwoordKandidaatService;
            _userManager = userManager;
            _context = context;
            _logger = logger;
        }

        

        public async Task<IActionResult> Index()
        {
            
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var vragen = await _antwoordKandidaatService.GetVragenByNaamCategorieAsync("Personalia");
            var antwoorden = await _context.AntwoordenKandidaten
                .Where(a => a.UserId == userId && a.Categorie.StartsWith("Personalia"))
                .ToListAsync();

            var model = new AntwoordKandidaatIndexViewModel
            {
                UserId = userId,
                Voornaam = user.Voornaam,
                Familienaam = user.Familienaam,
                VragenAntwoorden = vragen.Select(v => new VraagAntwoordKandidaatViewModel
                {
                    VraagId = v.Id,
                    VraagText = v.VraagText,
                    Type = v.SoortAntwoord.ToString(),
                    Opties = v.AntwoordOptie.OptieTekst?.Split(", ").ToList(),
                    Antwoord = antwoorden.FirstOrDefault(a => a.VraagKandidaatId == v.Id)?.AntwoordTekst,
                    Antwoorden = antwoorden.FirstOrDefault(a => a.VraagKandidaatId == v.Id)?.AntwoordTekst?.Split(", ").ToList()
                }).ToList(),

            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAntwoorden(List<VraagAntwoordKandidaatViewModel> vragenAntwoorden)
        {
            var userId = _userManager.GetUserId(User);
            foreach(var item in vragenAntwoorden)
            {
                if(item.UserId != userId)
                {
                    _logger.LogWarning("Unauthorized attempt to save answers by user {UserId}", userId);
                    return Unauthorized();
                }

                _logger.LogInformation("Saving answer for VraagId: {VraagId}, Antwoord: {Antwoord}, Antwoorden: {Antwoorden}",
                   item.VraagId, item.Antwoord, string.Join(", ", item.Antwoorden ?? new List<string>()));

                //controleer of antwoord niet null of leeg is
                if (string.IsNullOrEmpty(item.Antwoord) && (item.Antwoorden == null || !item.Antwoorden.Any()))
                    {
                    _logger.LogInformation("Skipping empty answer for VraagId: {VraagId}", item.VraagId);
                    continue;
                }

                //stel antwoord-eigenschap in voor checkboxen
                if(item.Type == "Checkboxen" && item.Antwoorden != null)
                {
                    item.Antwoord = string.Join(", ", item.Antwoorden);
                }

                //opslaan in MySQL
                var bestaandAntwoord = await _context.AntwoordenKandidaten
                    .FirstOrDefaultAsync(a => a.UserId == userId && a.VraagKandidaatId == item.VraagId);

                if(bestaandAntwoord != null)
                {
                    bestaandAntwoord.AntwoordTekst = item.Antwoord;
                    _logger.LogInformation("Updated existing answer for VraagId: {VraagId} with value: {Antwoord}", item.VraagId, bestaandAntwoord.AntwoordTekst);
                }
                else
                {
                    var vraag = await _antwoordKandidaatService.GetVraagKandidaatByIdAsync(item.VraagId);
                    if(vraag == null)
                    {
                        _logger.LogError("Vraag with Id {VraagId} not found", item.VraagId);
                        return NotFound($"Vraag with Id {item.VraagId} not found");
                    }
                    if(vraag.Categorie == null)
                    {
                        _logger.LogError("Categorie for Vraag with Id {VraagId} is null", item.VraagId);
                        _logger.LogInformation("Categorie for VraagId {VraagId} is {CategorieNaam}", item.VraagId, vraag.Categorie.NaamCategorie);
                        return StatusCode(500, $"Categorie for Vraag with Id {item.VraagId} is null");
                    }
                   
                    var nieuwAntwoord = new AntwoordKandidaat
                    {
                        UserId = userId,
                        VraagKandidaatId = item.VraagId,
                        AntwoordTekst = item.Antwoord,
                        Categorie = vraag.Categorie.NaamCategorie + " - " + vraag.Categorie.NaamSubCategorie,
                        DatumIngevuld = DateTime.Now,
                    };

                    _context.AntwoordenKandidaten.Add(nieuwAntwoord);
                    _logger.LogInformation("Added new answer for VraagId: {VraagId} with value: {Antwoord}", item.VraagId, nieuwAntwoord.AntwoordTekst);
                }
                
            }

            await _context.SaveChangesAsync();
            _logger.LogInformation("Answers saved successfully for user {UserId}", userId);
            return RedirectToAction("Index", "PersonaliaKandidaat");
        }
    }
}
