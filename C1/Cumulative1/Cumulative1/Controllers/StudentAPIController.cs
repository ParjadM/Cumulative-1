using Microsoft.AspNetCore.Mvc;
using Cumulative1.Model;
using MySql.Data.MySqlClient;
using System.Diagnostics;
namespace Cumulative1.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly SchoolDbContext _context;

        // Dependency injection of database context
        public StudentAPIController(SchoolDbContext context)
        {
            _context =context;
        }

        /// <summary>
        /// Returns detailed information about a specific student by ID.
        /// </summary>
        /// <example>
        /// GET api/Student/FindStudent/1 
        /// </example>
        /// <param name="id">The ID of the student to retrieve.</param>
        /// <returns>
        /// A student object containing detailed information.
        /// </returns>
        [HttpGet]
        [Route("FindStudent/{id}")]
        public Student FindStudent(int id)
        {
            // Initialize an empty Student object
            Student selectedStudent = null;

            using (MySqlConnection connection =_context.AccessDatabase())
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText= "SELECT * FROM Students WHERE StudentId = @id";
                command.Parameters.AddWithValue("@id", id);

                using (MySqlDataReader resultSet = command.ExecuteReader())
                {
                    // If a matching student is found, populate the student object
                    if (resultSet.Read())
                    {
                        selectedStudent = new Student
                        {
                            Id = Convert.ToInt32(resultSet["StudentId"]),
                            Name = $"{resultSet["studentfname"]} {resultSet["studentlname"]}",
                            Studentnumber = resultSet["studentnumber"].ToString(),
                            Enroldate = Convert.ToDateTime(resultSet["enroldate"])
                        };
                    }
                }
            }

            // Return the selected student or null if not found
            return selectedStudent;
        }

        /// <summary>
        /// Returns a list of all students in the system.
        /// </summary>
        /// <example>
        /// GET api/Student/ListStudents
        /// </example>
        /// <returns>
        /// A list of all student objects.
        /// </returns>
        [HttpGet]
        [Route("ListStudents")]
        public List<Student> ListStudents(string SearchKey = null)
        {
            // Create an empty list of students
            List<Student> students = new List<Student>();

            
            using (MySqlConnection connection = _context.AccessDatabase())
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                //command.CommandText = "SELECT * FROM students";

                string query = "SELECT * FROM students";

                //searh criteria
                if (SearchKey != null)
                {
                    query += " where lower(studentfname) like lower(@key) or lower(studentlname) like lower(@key) or lower(concat(studentfname,' ',studentlname)) like lower(@key) ";
                    command.Parameters.AddWithValue("@key", $"%{SearchKey}%");
                }
                Debug.WriteLine($"SearchKey: {SearchKey}");

                command.CommandText = query;
                command.Prepare();
                Debug.WriteLine(query);

                using (MySqlDataReader resultSet= command.ExecuteReader())
                {
                    // Loop through each row in the result set and populate the list
                    while (resultSet.Read())
                    {
                        students.Add(new Student
                        {
                            Id = Convert.ToInt32(resultSet["StudentId"]),
                            Name = $"{resultSet["studentfname"]} {resultSet["studentlname"]}",
                            Studentnumber = resultSet["studentnumber"].ToString(),
                            Enroldate = Convert.ToDateTime(resultSet["enroldate"])
                        });
                    }
                }
            }

            // Return the final list of students
            return students;
        }
    }
}
