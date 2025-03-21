using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cumulative1.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Cumulative1.Controllers
{
    [Route("api/Course")]
    [ApiController]
    public class CourseAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        // Dependency injection of database context
        public CourseAPIController(SchoolDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns detailed information about a specific course by ID.
        /// </summary>
        /// <example>
        /// GET api/Course/FindCourse/1
        /// </example>
        /// <param name="id">The ID of the course to retrieve.</param>
        /// <returns>
        /// A course object containing detailed information.
        /// </returns>
        [HttpGet]
        [Route("FindCourse/{id}")]
        public Course FindCourse(int id)
        {
            // Initialize an empty Course object
            Course selectedCourse = null;

            using (MySqlConnection connection =_context.AccessDatabase())
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText ="SELECT * FROM Courses WHERE Courseid = @id";
                command.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader resultSet= command.ExecuteReader())
                {
                    // If a matching course is found, populate the course object
                    if (resultSet.Read())
                    {
                        selectedCourse = new Course
                        {
                            Id = Convert.ToInt32(resultSet["Courseid"]),
                            Name = resultSet["coursename"].ToString(),
                            Teacherid = Convert.ToInt32(resultSet["teacherid"]),
                            Startdate = Convert.ToDateTime(resultSet["startdate"]),
                            Finishdate = Convert.ToDateTime(resultSet["finishdate"]),
                            Coursecode = resultSet["coursecode"].ToString()
                        };
                    }
                }
            }

            // Return the selected course or null if not found
            return selectedCourse;
        }

        /// <summary>
        /// Returns a list of all courses in the system.
        /// </summary>
        /// <example>
        /// GET api/Course/ListCourses 
        /// </example>
        /// <returns>
        /// A list of all course objects.
        /// </returns>
        [HttpGet]
        [Route("ListCourses")]
        public List<Course> ListCourses(string SearchKey = null)
        {
            // Create an empty list of courses
            List<Course>courses = new List<Course>();

            using (MySqlConnection connection= _context.AccessDatabase())
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                //command.CommandText = "SELECT * FROM courses";

                string query = "SELECT * FROM courses";

                //searh criteria
                if (SearchKey != null)
                {
                    query += " where lower(coursename) like lower(@key)";
                    command.Parameters.AddWithValue("@key", $"%{SearchKey}%");
                }
                Debug.WriteLine($"SearchKey: {SearchKey}");

                command.CommandText = query;
                command.Prepare();
                Debug.WriteLine(query);

                using (MySqlDataReader resultSet = command.ExecuteReader())
                {
                    //loop through each row in the result set and populate the list
                    while (resultSet.Read())
                    {
                        courses.Add(new Course
                        {
                            Id = Convert.ToInt32(resultSet["Courseid"]),
                            Name = resultSet["coursename"].ToString(),
                            Teacherid = Convert.ToInt32(resultSet["teacherid"]),
                            Startdate = Convert.ToDateTime(resultSet["startdate"]),
                            Finishdate = Convert.ToDateTime(resultSet["finishdate"]),
                            Coursecode = resultSet["coursecode"].ToString()
                        });
                    }
                }
            }

            //Return the final list of courses
            return courses;
        }
    }
}
