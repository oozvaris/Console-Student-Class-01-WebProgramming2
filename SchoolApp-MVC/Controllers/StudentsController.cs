using Microsoft.AspNetCore.Mvc;

namespace SchoolApp_MVC.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
