using StudentSectionApp.Application.DTOs;
using System.Collections.Generic;

namespace StudentSectionApp.Application.Interfaces
{
    public interface ISectionService
    {
        void AddSection(SectionDto sectionDto);
        void RemoveSection(int sectionId);
        void UpdateSection(SectionDto sectionDto);
        IEnumerable<SectionDto> GetAllSections();
        void AddStudentToSection(int studentId, int sectionId);
        IEnumerable<StudentDto> GetStudentsBySection(int sectionId);
        SectionDto GetSectionById(int sectionId);
    }
}