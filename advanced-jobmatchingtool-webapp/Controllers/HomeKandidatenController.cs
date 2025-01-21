using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace advanced_jobmatchingtool_webapp.Controllers
{
    [Authorize(Roles ="Kandidaat")]
    public class HomeKandidatenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
