using StudentSectionApp.Domain.Entities;
using StudentSectionApp.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace StudentSectionApp.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly List<Student> _students = new List<Student>();

        public void AddStudent(Student student)
        {
            _students.Add(student);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _students;
        }

        public Student GetStudentById(int studentId)
        {
            return _students.FirstOrDefault(s => s.Id == studentId)!;
        }

        public void RemoveStudent(int studentId)
        {
            var student = GetStudentById(studentId);
            if (student != null)
            {
                _students.Remove(student);
            }
        }

        public void UpdateStudent(Student student)
        {
            var existingStudent = GetStudentById(student.Id);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Age = student.Age;
                existingStudent.Email = student.Email;
                existingStudent.Address = student.Address;
                existingStudent.PhoneNumber = student.PhoneNumber;
                existingStudent.EnrollmentDate = student.EnrollmentDate;
                existingStudent.SectionId = student.SectionId;
            }
        }
    }
}