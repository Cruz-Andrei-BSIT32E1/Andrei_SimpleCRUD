using Microsoft.AspNetCore.Mvc;
using StudentSectionApp.Application.DTOs;
using StudentSectionApp.Application.Interfaces; // Correct namespace reference

namespace StudentSectionApp.Presentation.Controllers
{
    public class SectionsController : Controller
    {
        private readonly ISectionService _sectionService;

        public SectionsController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        public IActionResult Index()
        {
            var sections = _sectionService.GetAllSections();
            return View(sections);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SectionDto sectionDto)
        {
            if (ModelState.IsValid)
            {
                _sectionService.AddSection(sectionDto);
                return RedirectToAction(nameof(Index));
            }
            return View(sectionDto);
        }

        public IActionResult Edit(int id)
        {
            var section = _sectionService.GetSectionById(id);
            if (section == null)
            {
                return NotFound();
            }
            return View(section);
        }

        [HttpPost]
        public IActionResult Edit(SectionDto sectionDto)
        {
            if (ModelState.IsValid)
            {
                _sectionService.UpdateSection(sectionDto);
                return RedirectToAction(nameof(Index));
            }
            return View(sectionDto);
        }

        public IActionResult Delete(int id)
        {
            var section = _sectionService.GetSectionById(id);
            if (section == null)
            {
                return NotFound();
            }
            return View(section);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var section = _sectionService.GetSectionById(id);
            if (section.StudentIds.Count == 0)
            {
                _sectionService.RemoveSection(id);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}