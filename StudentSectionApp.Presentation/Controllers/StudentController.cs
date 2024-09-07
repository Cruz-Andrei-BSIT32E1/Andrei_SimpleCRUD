using Microsoft.AspNetCore.Mvc;
using StudentSectionApp.Application.DTOs;
using StudentSectionApp.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace StudentSectionApp.Presentation.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;

        public StudentController(IStudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var students = _studentService.GetAllStudents();
            _logger.LogInformation("Fetched {Count} students", students.Count());
            return View("/Views/Students/Index.cshtml", students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("/Views/Students/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StudentDto studentDto)
        {
            var students = _studentService.GetAllStudents();
            var newId = students.Any() ? students.Max(s => s.Id) + 1 : 1;
            studentDto.Id = newId;

            if (ModelState.IsValid)
            {
                _studentService.AddStudent(studentDto);
                _logger.LogInformation("Added student: {Id}, {Name}, {Age}, {Email}, {Address}, {PhoneNumber}, {SectionName}", 
                    studentDto.Id, studentDto.Name, studentDto.Age, studentDto.Email, studentDto.Address, studentDto.PhoneNumber, studentDto.SectionName);
                return RedirectToAction(nameof(Index));
            }
            return View("/Views/Students/Create.cshtml", studentDto);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Json(student);
        }

       [HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Edit(StudentDto studentDto)
{
    if (ModelState.IsValid)
    {
        try
        {
            _studentService.UpdateStudent(studentDto);
            _logger.LogInformation("Updated student: {Id}, {Name}, {Age}, {Email}, {Address}, {PhoneNumber}, {SectionName}", 
                studentDto.Id, studentDto.Name, studentDto.Age, studentDto.Email, studentDto.Address, studentDto.PhoneNumber, studentDto.SectionName);
            return Json(new { success = true, student = studentDto });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating student with ID: {Id}", studentDto.Id);
            return Json(new { success = false, message = "An error occurred while updating the student. Please try again later." });
        }
    }
    else
    {
        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
        _logger.LogWarning("Invalid data for student with ID: {Id}. Errors: {Errors}", studentDto.Id, string.Join(", ", errors));
        return Json(new { success = false, message = "Invalid data", errors });
    }
}

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var student = _studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View("/Views/Students/Delete.cshtml", student);
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }

            // Log the SectionId value
            _logger.LogInformation("Student ID: {Id}, SectionId: {SectionId}", student.Id, student.SectionId);

            try
            {
                _studentService.RemoveStudent(id);
                _logger.LogInformation("Deleted student with ID: {Id}", id);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning("Attempted to delete student with ID: {Id} who is assigned to a section. SectionId: {SectionId}", id, student.SectionId);
                return BadRequest(ex.Message);
            }
        }
    }
}