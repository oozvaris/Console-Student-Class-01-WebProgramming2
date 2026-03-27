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
                               SELECT StudentID, StudentName, StudentSurename, StudentEmail 
                               FROM Student
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
                    StudentSurename = reader.GetString(reader.GetOrdinal("StudentSurename")),
                    StudentEmail = reader.GetString(reader.GetOrdinal("StudentEmail"))
                };
                students.Add(student);
            }

            return students;
        }
    }
}
