using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DAL.Data.Interfaces;
using DAL.Models;

namespace SchoolApp_MVC.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<IReadOnlyList<Student>> DisplayStudentListAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            return students;
            
        }

        public async Task AddStudentAsync(Student student)
        {
            int newStudentId = await _studentRepository.CreateAsync(student);
            Console.WriteLine($"New student added with ID: {newStudentId}");
            Console.WriteLine("------------------------------------------------");
        }

        public Student RegisterStudent()
        {
            Console.WriteLine("Enter student name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter student surname:");
            string surname = Console.ReadLine();
            Console.WriteLine("Enter student email:");
            string email = Console.ReadLine();
            return new Student
            {
                StudentName = name,
                StudentSurname = surname,
                StudentEmail = email
            };

        }

        public async Task DeleteStudentAsync(int studentId)
        {
            await _studentRepository.DeleteAsync(studentId);
            Console.WriteLine($"Student with ID {studentId} has been deleted.");
            Console.WriteLine("------------------------------------------------");

        }

        public async Task UpdateStudentAsync(Student student)
        {
            await _studentRepository.UpdateAsync(student);
            Console.WriteLine($"Student with ID {student.StudentID} has been updated.");
            Console.WriteLine("------------------------------------------------");
        }

        public async Task<Student> FindStudentByIdAsync(int studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);
            if (student != null)
            {
                Console.WriteLine(
                    $"Student found: ID = {student.StudentID}, " +
                    $"Name = {student.StudentName}, " +
                    $"Surname = {student.StudentSurname}, " +
                    $"Email = {student.StudentEmail}");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
            Console.WriteLine("------------------------------------------------");
            return student;
        }

     

    }
}
