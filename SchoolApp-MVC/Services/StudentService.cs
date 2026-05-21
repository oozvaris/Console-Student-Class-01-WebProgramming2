using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DAL.Data.Interfaces;
using DAL.Models;
using SchoolApp_MVC.Dtos.Students;

namespace SchoolApp_MVC.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IReadOnlyList<StudentReadDto>> DisplayStudentListAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            return students.Select(MapToReadDto).ToList();
            
        }

        public async Task<bool> AddStudentAsync(Student student)
        {
            int newStudentId = await _studentRepository.CreateAsync(student);
            Console.WriteLine($"New student added with ID: {newStudentId}");
            Console.WriteLine("------------------------------------------------");

            return true;
        }

        public async Task DeleteStudentAsync(int studentId)
        {
            await _studentRepository.DeleteAsync(studentId);
            Console.WriteLine($"Student with ID {studentId} has been deleted.");
            Console.WriteLine("------------------------------------------------");

        }

        public async Task<bool> UpdateStudentAsync(Student student)
        {
            var result = await _studentRepository.UpdateAsync(student);

            Console.WriteLine($"Student with ID {student.StudentID} has been updated.");
            Console.WriteLine("------------------------------------------------");
            return result;
        }

        public async Task<StudentReadDto?> FindStudentByIdAsync(int studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
           
            return student is null ? null : MapToReadDto(student);
        }

        private static StudentReadDto MapToReadDto(Student student)
        {
            return new StudentReadDto
            {
                StudentID = student.StudentID,
                StudentName = student.StudentName,
                StudentSurname = student.StudentSurname,
                StudentEmail = student.StudentEmail
            };
        }



    }
}
