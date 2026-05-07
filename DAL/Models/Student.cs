using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; } = string.Empty;

        public string StudentSurname { get; set; } = string.Empty;

        public string StudentEmail { get; set; } = string.Empty;

    }
}
