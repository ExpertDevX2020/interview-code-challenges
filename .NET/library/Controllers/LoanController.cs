using Microsoft.AspNetCore.Mvc;

namespace OneBeyondApi.Controllers
{
    public class LoanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
