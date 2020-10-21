using Microsoft.AspNetCore.Mvc;

namespace ISPH.API.Controllers.ViewControllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
           return View();
        }

        public IActionResult Advertisements()
        {
            return View("Index");
        }

        public IActionResult RegistrationForm()
        {
            return PartialView("RegistrationForm");
        }
        public IActionResult AuthorisationForm()
        {
            return PartialView("AuthorisationForm");
        }

        public IActionResult Profile()
        {
            return View("Index");
        }

        public IActionResult Articles()
        {
            return View("Index");
        }

        public IActionResult News()
        {
            return View("Index");
        }
    }
}
