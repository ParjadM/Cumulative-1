using Microsoft.AspNetCore.Mvc;
using Cumulative1.Model;

namespace Cumulative1.Controllers
{
    public class TeacherPageController : Controller
    {
        private readonly TeacherAPIController _api;
        public TeacherPageController(TeacherAPIController api)
        {
            _api = api; 
        }

        /// <summary>
        /// Displays a list of teachers on a dynamic page.
        /// </summary>
        /// <returns>A view containing all teachers.</returns>
        public IActionResult List()
        {
            List<Teacher> Teachers =_api.ListTeachers();
            return View("~/Views/Teacher/List.cshtml", Teachers);
        }


        /// <summary>
        /// Displays details of a specific teacher by their ID.
        /// </summary>
        /// <param name="id">The ID of the teacher.</param>
        /// <returns>A view containing the teacher's details.</returns>
        public IActionResult Show(int id)
        {
            Teacher SelectedTeacher = _api.FindTeacher(id);
            return View("~/Views/Teacher/Show.cshtml",SelectedTeacher);
        }

    }
}
