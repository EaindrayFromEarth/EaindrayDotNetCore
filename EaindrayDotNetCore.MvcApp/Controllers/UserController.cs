using Microsoft.AspNetCore.Mvc;

namespace EaindrayDotNetCore.MvcApp.Controllers
{
    public class UserController : Controller
    {
        [ActionName("Index")]
        public IActionResult UserIndex()
        {
            return View("UserIndex");
        }
    }
}
