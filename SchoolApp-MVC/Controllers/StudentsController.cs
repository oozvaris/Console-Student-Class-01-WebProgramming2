using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using SchoolApp_MVC.Services;

namespace SchoolApp_MVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;

            // Student s = _studentService.FindStudentByIdAsync(1).GetAwaiter().GetResult();

        }

        public IActionResult Index()
        {
            var students = _studentService.DisplayStudentListAsync().GetAwaiter().GetResult();

            return View(students);
        }

        //public async Task<IActionResult> Index()
        //{
        //    var students = await _studentService.DisplayStudentListAsync();

        //    return View(students);
        //}

        public IActionResult StudentsList(int id)
        {
            ViewData["id"] = id;
            return View();
        }
    }
}
