using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace advanced_jobmatchingtool_webapp.Controllers.Klant
{
    [Authorize(Roles = "Klant")]
    public class HomeKlantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
