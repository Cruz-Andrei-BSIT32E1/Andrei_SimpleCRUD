using System.Collections.Generic;
using StudentSectionApp.Domain.Entities;

namespace StudentSectionApp.Domain.Interfaces
{
    public interface IStudentRepository
    {
        void AddStudent(Student student);
        void RemoveStudent(int studentId);
        void UpdateStudent(Student student);
        Student GetStudentById(int studentId);
        IEnumerable<Student> GetAllStudents();
    }
}