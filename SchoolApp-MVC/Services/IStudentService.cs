using DAL.Models;

namespace SchoolApp_MVC.Services
{
    public interface IStudentService
    {
        Task<IReadOnlyList<Student>> DisplayStudentListAsync();
        Task AddStudentAsync(Student student);
        Student RegisterStudent();
        Task DeleteStudentAsync(int studentId);

        Task UpdateStudentAsync(Student student);        

        Task<Student> FindStudentByIdAsync(int studentId);
    }
}
