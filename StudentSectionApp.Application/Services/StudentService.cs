using StudentSectionApp.Application.DTOs;
using StudentSectionApp.Application.Interfaces;
using StudentSectionApp.Domain.Entities;
using StudentSectionApp.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace StudentSectionApp.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ISectionRepository _sectionRepository;

        public StudentService(IStudentRepository studentRepository, ISectionRepository sectionRepository)
        {
            _studentRepository = studentRepository;
            _sectionRepository = sectionRepository;
        }

        public void AddStudent(StudentDto studentDto)
        {
            // Fetch the highest existing student ID and increment it by one
            var students = _studentRepository.GetAllStudents();
            var newId = students.Any() ? students.Max(s => s.Id) + 1 : 1;
            
            var section = _sectionRepository.GetSectionByName(studentDto.SectionName!);
            var student = new Student
            {
                Id = newId, // Assign the new unique ID
                Name = studentDto.Name,
                Age = studentDto.Age,
                Email = studentDto.Email,
                Address = studentDto.Address,
                PhoneNumber = studentDto.PhoneNumber,
                EnrollmentDate = studentDto.EnrollmentDate,
                SectionId = studentDto.SectionId
            };

            // Log the SectionId and SectionName
            Console.WriteLine($"Adding student: {student.Id}, {student.Name}, SectionId: {student.SectionId}, SectionName: {studentDto.SectionName}");

            _studentRepository.AddStudent(student);
        }

        public IEnumerable<StudentDto> GetAllStudents()
        {
            return _studentRepository.GetAllStudents().Select(student => new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                Email = student.Email,
                Address = student.Address,
                PhoneNumber = student.PhoneNumber,
                EnrollmentDate = student.EnrollmentDate,
                SectionName = student.SectionId 
            });
        }

        public StudentDto GetStudentById(int studentId)
        {
            var student = _studentRepository.GetStudentById(studentId);
            if (student == null) return null!;

            return new StudentDto
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                Email = student.Email,
                Address = student.Address,
                PhoneNumber = student.PhoneNumber,
                EnrollmentDate = student.EnrollmentDate,
                SectionName = student.SectionId
            };
        }

public void RemoveStudent(int studentId)
{
    var student = _studentRepository.GetStudentById(studentId);
    if (student == null) return;

    if (!string.IsNullOrEmpty(student.SectionId))
    {
        // Log the attempt to delete a student with a SectionId
        Console.WriteLine($"Cannot delete student: {student.Id}, {student.Name}, SectionId: {student.SectionId}");
        throw new InvalidOperationException("Cannot delete a student who is assigned to a section.");
    }

    _studentRepository.RemoveStudent(studentId);
}
public void UpdateStudent(StudentDto studentDto)
{
    // Fetch the student entity from the repository
    var student = _studentRepository.GetStudentById(studentDto.Id);
    if (student == null) return;

    // Fetch the section entity using the section name
    var section = _sectionRepository.GetSectionByName(studentDto.SectionName!);
    if (section == null) return;

    // Update the student entity with the new values from the DTO
    student.Name = studentDto.Name;
    student.Age = studentDto.Age;
    student.Email = studentDto.Email;
    student.Address = studentDto.Address;
    student.PhoneNumber = studentDto.PhoneNumber;
    student.EnrollmentDate = studentDto.EnrollmentDate; // Ensure EnrollmentDate is updated
    student.SectionId = section.Id.ToString(); // Convert section.Id to string

    // Log the SectionId and SectionName
    Console.WriteLine($"Updating student: {student.Id}, {student.Name}, SectionId: {student.SectionId}, SectionName: {studentDto.SectionName}");

    // Persist the changes to the repository
    _studentRepository.UpdateStudent(student);
}
    }
}