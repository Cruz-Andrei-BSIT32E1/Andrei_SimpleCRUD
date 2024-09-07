using System.Collections.Generic;
using System.Linq;
using StudentSectionApp.Application.DTOs;
using StudentSectionApp.Application.Interfaces;
using StudentSectionApp.Domain.Entities;
using StudentSectionApp.Domain.Interfaces;

namespace StudentSectionApp.Application.Services
{
    public class SectionService : ISectionService 
    {
        private readonly ISectionRepository_sectionRepository;
        private readonly IStudentRepository_studentRepository;

        public SectionService(ISectionRepository sectionRepository, IStudentRepository studentRepository)
        {
            _sectionRepository = sectionRepository;
            _studentRepository = studentRepository;
        }

        public void AddSection(SectionDto sectionDto)
        {
            var section = new Section { Id = sectionDto.Id, Name = sectionDto.Name, StudentIds = sectionDto.StudentIds };
            _sectionRepository.AddSection(section);
        }

        public void RemoveSection(int sectionId)
        {
            var section = _sectionRepository.GetSectionById(sectionId);
            if (section != null && !section.StudentIds.Any())
            {
                _sectionRepository.RemoveSection(sectionId);
            }
        }

        public void UpdateSection(SectionDto sectionDto)
        {
            var section = new Section { Id = sectionDto.Id, Name = sectionDto.Name, StudentIds = sectionDto.StudentIds };
            _sectionRepository.UpdateSection(section);
        }

        public void AddStudentToSection(int studentId, int sectionId)
        {
            var student = _studentRepository.GetStudentById(studentId);
            var section = _sectionRepository.GetSectionById(sectionId);

            if (student != null && section != null)
            {
                student.SectionId = sectionId.ToString();
                section.StudentIds.Add(studentId);
                _studentRepository.UpdateStudent(student);
                _sectionRepository.UpdateSection(section);
            }
        }

        public IEnumerable<StudentDto> GetStudentsBySection(int sectionId)
        {
            var section = _sectionRepository.GetSectionById(sectionId);
            if (section != null)
            {
                return section.StudentIds.Select(id => _studentRepository.GetStudentById(id)).Select(s => new StudentDto { Id = s.Id, Name = s.Name, SectionId = s.SectionId });
            }
            return Enumerable.Empty<StudentDto>();
        }

        public SectionDto GetSectionById(int sectionId)
        {
            var section = _sectionRepository.GetSectionById(sectionId);
            if (section != null)
            {
                return new SectionDto { Id = section.Id, Name = section.Name, StudentIds = section.StudentIds };
            }
            return null!;
        }
    }
}