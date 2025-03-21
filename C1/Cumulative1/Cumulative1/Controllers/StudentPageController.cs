using Cumulative1.Model;
using Microsoft.AspNetCore.Mvc;

namespace Cumulative1.Controllers
{
    public class StudentPageController : Controller
    {
        
        private readonly StudentAPIController _api;

        
        public StudentPageController(StudentAPIController api)
        {
            _api =api;
        }

        /// <summary>
        /// Displays a list of Students on a dynamic page.
        /// </summary>
        /// <returns>A view containing all Students.</returns>
        public IActionResult List()
        {
            
            List<Student> Students = _api.ListStudents();

            
            return View("~/Views/Student/List.cshtml", Students);
        }


        /// <summary>
        /// Displays details of a specific Student by their ID.
        /// </summary>
        /// <param name="id">The ID of the Student.</param>
        /// <returns>A view containing the Student's details.</returns>
        public IActionResult Show(int id)
        {
            Student SelectedStudent =_api.FindStudent(id);
            return View("~/Views/Student/Show.cshtml", SelectedStudent);
        }
    }
}
