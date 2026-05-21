using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using SchoolApp_MVC.Dtos.Students;
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

        //public IActionResult Index()
        //{
        //    var students = _studentService.DisplayStudentListAsync().GetAwaiter().GetResult();

        //    return View(students);
        //}

        public async Task<IActionResult> Index()
        {
            var students = await _studentService.DisplayStudentListAsync();

            return View(students);
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.FindStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.FindStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            var studentUpdateDto = new StudentUpdateDto
            {
                StudentID = student.StudentID,
                StudentName = student.StudentName,
                StudentSurname = student.StudentSurname,
                StudentEmail = student.StudentEmail
            };

            return View(studentUpdateDto);

        }

        [HttpPost]
        public async Task<IActionResult> Edit (int id, StudentUpdateDto studentUpdateDto)
        {
            if (id != studentUpdateDto.StudentID)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View(studentUpdateDto);
            }
            var studentToUpdate = new Student
            {
                StudentID = studentUpdateDto.StudentID,
                StudentName = studentUpdateDto.StudentName,
                StudentSurname = studentUpdateDto.StudentSurname,
                StudentEmail = studentUpdateDto.StudentEmail
            };

            var result = await _studentService.UpdateStudentAsync(studentToUpdate);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Update operation failed.");
                return View(studentUpdateDto);
            }

            TempData["SuccessMessage"] = "Student updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.FindStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            await _studentService.DeleteStudentAsync(id);

            TempData["SuccessMessage"] = "Student deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View(new StudentCreateDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create(StudentCreateDto studentCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return View(studentCreateDto);
            }
            var studentToCreate = new Student
            {
                // StudentID = 0,
                StudentName = studentCreateDto.StudentName,
                StudentSurname = studentCreateDto.StudentSurname,
                StudentEmail = studentCreateDto.StudentEmail
            };

            var result = await _studentService.AddStudentAsync(studentToCreate);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Create operation failed.");
                return View(studentCreateDto);
            }

            TempData["SuccessMessage"] = "Student crated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult StudentsList(int id)
        {
            ViewData["id"] = id;
            return View();
        }
    }
}
