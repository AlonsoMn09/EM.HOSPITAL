using Microsoft.AspNetCore.Mvc;

namespace EM.Hospital.API.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
