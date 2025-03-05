using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace advanced_jobmatchingtool_webapp.Controllers
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
