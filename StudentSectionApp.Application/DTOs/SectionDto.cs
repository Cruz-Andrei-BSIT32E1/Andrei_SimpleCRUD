namespace StudentSectionApp.Application.DTOs
{
    public class SectionDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public HashSet<int> StudentIds { get; set; } = new HashSet<int>();
    }
}