using Microsoft.AspNetCore.Mvc;


namespace AspNetInterop.UI.Core.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Forbidden()
        {
            return View("Forbidden");
        }
    }
}
