using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DAL.Data.Interfaces;
using DAL.Models;

namespace Console_Student_Class_01
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task DisplayStudentListAsync()
        {
            var students = await _studentRepository.GetAllAsync();



            foreach (Student s in students)
            {
                Console.WriteLine("Student List Item");
                Console.WriteLine("Student ID : " + s.StudentID);
                Console.WriteLine("Student Name : " + s.StudentName);
                Console.WriteLine("Student Surename : " + s.StudentSurename);
                Console.WriteLine("Student Email : " + s.StudentEmail);
                Console.WriteLine("------------------------------------------------");
            }

            Console.WriteLine("Total number of students: " + students.Count);
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
                StudentSurename = surname,
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
                    $"Surname = {student.StudentSurename}, " +
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
