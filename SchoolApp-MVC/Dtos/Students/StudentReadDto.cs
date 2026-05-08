namespace SchoolApp_MVC.Dtos.Students
{
    public class StudentReadDto
    {
        public int StudentID { get; set; }
        public string StudentName { get; set; } = null!;
        public string StudentSurname { get; set; } = null!;
        public string StudentEmail { get; set; } = null!;
    }
}
