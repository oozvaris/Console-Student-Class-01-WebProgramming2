using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL.Models;

namespace DAL.Data.Interfaces
{
    public interface IStudentRepository
    {
        Task<IReadOnlyList<DAL.Models.Student>> GetAllAsync();
        //Task<Student> GetByIdAsync(int id);
        //Task<Student> GetByNameAsync(string name);
        //Task<Student> GetByEmailAsync(string email);
        Task<int> CreateAsync(Student student);
        //Task UpdateAsync(Student student);
        //Task DeleteAsync(int id);

    }
}
