using DAL.Models;
using SchoolApp_MVC.Dtos.Students;

namespace SchoolApp_MVC.Services
{
    public interface IStudentService
    {
        Task<IReadOnlyList<StudentReadDto>> DisplayStudentListAsync();
        Task AddStudentAsync(Student student);
        Student RegisterStudent();
        Task DeleteStudentAsync(int studentId);

        Task<bool> UpdateStudentAsync(Student student);

        Task<StudentReadDto?> FindStudentByIdAsync(int studentId);
    }
}
