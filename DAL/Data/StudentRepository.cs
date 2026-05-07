using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace DAL.Data
{
    public class StudentRepository : Interfaces.IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' was not found");
        }


        public async Task<IReadOnlyList<Models.Student>> GetAllAsync()
        {
            var students = new List<Student>();
            const string sql = """
                               SELECT StudentID, StudentName, StudentSurname, StudentEmail 
                               FROM Student
                               ORDER BY StudentName
                               """;
            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var student = new Student
                {
                    StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                    StudentName = reader.GetString(reader.GetOrdinal("StudentName")),
                    StudentSurname = reader.GetString(reader.GetOrdinal("StudentSurname")),
                    StudentEmail = reader.GetString(reader.GetOrdinal("StudentEmail"))
                };
                students.Add(student);
            }

            return students;
        }

        public async Task<int> CreateAsync(Student student)
        {
            const string sql = """
                               INSERT INTO Student (StudentName, StudentSurname, StudentEmail) 
                               VALUES (@StudentName, @StudentSurname, @StudentEmail);
                               SELECT SCOPE_IDENTITY();
                               """;

            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@StudentName", student.StudentName);
            command.Parameters.AddWithValue("@StudentSurname", student.StudentSurname);
            command.Parameters.AddWithValue("@StudentEmail", student.StudentEmail);

            await connection.OpenAsync();

            var result = await command.ExecuteScalarAsync();

            return Convert.ToInt32(result);
        }

        public async Task DeleteAsync(int id)
        {
            const string sql = "DELETE FROM Student WHERE StudentID = @StudentID";
            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@StudentID", id);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

        }

        public async Task UpdateAsync(Student student)
        {
            const string sql = """
                               UPDATE Student 
                               SET StudentName = @StudentName, StudentSurname = @StudentSurname, StudentEmail = @StudentEmail 
                               WHERE StudentID = @StudentID
                               """;
            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@StudentID", student.StudentID);
            command.Parameters.AddWithValue("@StudentName", student.StudentName);
            command.Parameters.AddWithValue("@StudentSurname", student.StudentSurname);
            command.Parameters.AddWithValue("@StudentEmail", student.StudentEmail);
            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

        }

        public async Task<Student> GetByIdAsync(int id)
        {
            const string sql = """
                               SELECT StudentID, StudentName, StudentSurname, StudentEmail 
                               FROM Student 
                               WHERE StudentID = @StudentID
                               """;
            await using var connection = new SqlConnection(_connectionString);
            await using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@StudentID", id);
            await connection.OpenAsync();
            await using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Student
                {
                    StudentID = reader.GetInt32(reader.GetOrdinal("StudentID")),
                    StudentName = reader.GetString(reader.GetOrdinal("StudentName")),
                    StudentSurname = reader.GetString(reader.GetOrdinal("StudentSurname")),
                    StudentEmail = reader.GetString(reader.GetOrdinal("StudentEmail"))
                };
            }
            
            return null;

            // throw new KeyNotFoundException($"Student with ID {id} not found.");


        }

    }
}
