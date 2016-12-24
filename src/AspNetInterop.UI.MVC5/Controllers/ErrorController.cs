
using System.Web.Mvc;

namespace AspNetInterop.UI.MVC5.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Forbidden()
        {
            return View();
        }
    }
}