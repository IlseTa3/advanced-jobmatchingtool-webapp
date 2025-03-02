using Microsoft.AspNetCore.Mvc;

namespace advanced_jobmatchingtool_webapp.Controllers
{
    public class HomeBeheerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
