namespace StudentSectionApp.Application.DTOs
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime EnrollmentDate { get; set;}
        public string? SectionId { get; set; }
        public string? SectionName { get; set; }
    }
}