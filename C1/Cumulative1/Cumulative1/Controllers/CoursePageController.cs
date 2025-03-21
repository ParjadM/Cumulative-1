using Cumulative1.Model;
using Microsoft.AspNetCore.Mvc;

namespace Cumulative1.Controllers
{
    public class CoursePageController : Controller
    {
        
        private readonly CourseAPIController _api;

        // Constructor for CoursePageController
        public CoursePageController(CourseAPIController api)
        {
            _api = api;
        }

        /// <summary>
        /// Displays a list of Courses on a dynamic page.
        /// </summary>
        /// <returns>A view containing all Courses.</returns>
        public IActionResult List()
        {
            
            List<Course> Courses = _api.ListCourses();
            return View("~/Views/Course/List.cshtml", Courses);
        }


        /// <summary>
        /// Displays details of a specific Course by their ID.
        /// </summary>
        /// <param name="id">The ID of the Course.</param>
        /// <returns>A view containing the Course's details.</returns>
        public IActionResult Show(int id)
        {
            
            Course SelectedCourse = _api.FindCourse(id);
            return View("~/Views/Course/Show.cshtml", SelectedCourse);
        }
    }
}
