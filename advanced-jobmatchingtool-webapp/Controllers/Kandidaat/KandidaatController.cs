using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Services.Kandidaat;
using advanced_jobmatchingtool_webapp.ViewModels.Kandidaat;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace advanced_jobmatchingtool_webapp.Controllers.Kandidaat
{
    [Authorize(Roles ="Voorlopige kandidaat,Kandidaat")]
    [Route("kandidaat")]
    public class KandidaatController : Controller
    {
        private readonly IKandidaatService _kandidaatService;
        private readonly UserManager<ApplicationUser> _userManager;

        public KandidaatController(IKandidaatService kandidaatService, UserManager<ApplicationUser> userManager)
        {
            _kandidaatService = kandidaatService;
            _userManager = userManager;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return RedirectToAction("Profiel");
        }

        [HttpGet("profiel")]
        public async Task<IActionResult> Profiel()
        {
            var userId = _userManager.GetUserId(User);
            var model = await _kandidaatService.GetProfielAsync(userId);
            return View(model);
        }



        [HttpPost("profiel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profiel(KandidaatProfielViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            if(!ModelState.IsValid)
            {
                //herladen bestaande data
                var bestaandModel = await _kandidaatService.GetProfielAsync(userId);
                model.Statuut.BestaandeBestanden = bestaandModel.Statuut.BestaandeBestanden;
                return View(model);
            }

            var success = await _kandidaatService.SaveProfielAsync(model,userId);
            if(success)
            {
                TempData["ProfielStatus"] = "Je profiel is succesvol opgeslagen.";
                return Redirect("/kandidaat/bevestiging");
            }

            ModelState.AddModelError("", "Er ging iets mis bij het opslaan.");
            return View(model);
        }

        [HttpGet("bevestiging")]
        public IActionResult ProfielBevestiging()
        {
            ViewBag.Status = TempData["ProfielStatus"];
            return View();
        }
    }
}
