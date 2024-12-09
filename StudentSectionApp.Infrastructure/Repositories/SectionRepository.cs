using System.Collections.Generic;
using System.Linq;
using StudentSectionApp.Domain.Entities;
using StudentSectionApp.Domain.Interfaces;

namespace StudentSectionApp.Infrastructure.Repositories
{
    public class SectionRepository : ISectionRepository
    {
        private readonly List<Section> _sections = new List<Section>
        {
            new Section { Id = 1, Name = "Math" },
            new Section { Id = 2, Name = "Science" },
            new Section { Id = 3, Name = "History" }
        };

        public void AddSection(Section section)
        {
            _sections.Add(section);
        }

        public void RemoveSection(int sectionId)
        {
            var section = _sections.FirstOrDefault(s => s.Id == sectionId);
            if (section != null)
            {
                _sections.Remove(section);
            }
        }

        public void UpdateSection(Section section)
        {
            var existingSection = _sections.FirstOrDefault(s => s.Id == section.Id);
            if (existingSection != null)
            {
                existingSection.Name = section.Name;
            }
        }

        public Section GetSectionById(int sectionId)
        {
            return _sections.FirstOrDefault(s => s.Id == sectionId)!;
        }

        public Section GetSectionByName(string name)
        {
            return _sections.FirstOrDefault(s => s.Name == name)!;
        }

        public IEnumerable<Section> GetAllSections()
        {
            return _sections;
        }
    }
}