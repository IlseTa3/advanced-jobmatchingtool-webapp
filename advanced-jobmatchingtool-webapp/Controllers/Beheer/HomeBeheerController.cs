using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace advanced_jobmatchingtool_webapp.Controllers.Beheer
{
    [Authorize(Roles = "Beheerder")]
    public class HomeBeheerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
