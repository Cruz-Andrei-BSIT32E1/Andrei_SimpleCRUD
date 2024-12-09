using System.Collections.Generic;
using StudentSectionApp.Domain.Entities;

namespace StudentSectionApp.Domain.Interfaces
{
    public interface ISectionRepository
    {
        void AddSection(Section section);
        void RemoveSection(int sectionId);
        void UpdateSection(Section section);
        Section GetSectionById(int sectionId);
        Section GetSectionByName(string name);
        IEnumerable<Section> GetAllSections();
    }
}