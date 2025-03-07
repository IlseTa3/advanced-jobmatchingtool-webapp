using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace advanced_jobmatchingtool_webapp.Controllers.Kandidaat
{
    public class HomeKandidaatController : Controller
    {
        [Authorize(Roles = "Kandidaat")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
