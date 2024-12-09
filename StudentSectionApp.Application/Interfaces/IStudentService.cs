using StudentSectionApp.Application.DTOs;
using System.Collections.Generic;

namespace StudentSectionApp.Application.Interfaces
{
    public interface IStudentService
    {
        void AddStudent(StudentDto studentDto);
        void RemoveStudent(int studentId);
        void UpdateStudent(StudentDto studentDto);
        IEnumerable<StudentDto> GetAllStudents();
        StudentDto GetStudentById(int studentId);
    }
}