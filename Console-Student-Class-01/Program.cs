using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Models;

namespace Console_Student_Class_01
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            var studentRepository = new StudentRepository(configuration);

            var studentService = new StudentService(studentRepository);

            List<Course> courseList = new List<Course>();
            List<Student> studentList = new List<Student>();           

            Student student = new Student();
            Course course = new Course();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Student Registration System");
                Console.WriteLine("1 - Do you want to add a new student");
                Console.WriteLine("2 - Display All Students");
                Console.WriteLine("3 - Find Student");
                Console.WriteLine("4 - Delete Student");
                Console.WriteLine("5 - Edit Student");
                Console.WriteLine("10 - Exit");
                string answer = Console.ReadLine();
                if (answer == "1")
                {
                    student = studentService.RegisterStudent();

                    studentService.AddStudentAsync(student).Wait();
                    
                }
                else if (answer == "2")
                {
                    Console.WriteLine("Displaying all students...");
                    studentService.DisplayStudentListAsync().Wait();

                }
                else if (answer == "3")
                {
                    Console.WriteLine("Finding student...");
                    Console.WriteLine("Enter student ID to find:");
                    int studentId = Convert.ToInt32(Console.ReadLine());
                    studentService.FindStudentByIdAsync(studentId).Wait();
                }
                else if (answer == "4")
                {
                    Console.WriteLine("Enter student ID to delete:");
                    int studentId = Convert.ToInt32(Console.ReadLine());
                    studentService.DeleteStudentAsync(studentId).Wait();
                }
                else if (answer == "5")
                {
                    Console.WriteLine("Enter student ID to update:");
                    int studentId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter new student name:");
                    string newName = Console.ReadLine();
                    Console.WriteLine("Enter new student surname:");
                    string newSurname = Console.ReadLine();
                    Console.WriteLine("Enter new student email:");
                    string newEmail = Console.ReadLine();
                    Student updatedStudent = new Student
                    {
                        StudentID = studentId,
                        StudentName = newName,
                        StudentSurename = newSurname,
                        StudentEmail = newEmail
                    };
                    studentService.UpdateStudentAsync(updatedStudent).Wait();

                }
                else if (answer == "10")
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }

            // Console.ReadLine();
        }
    }
}
