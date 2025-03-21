using MySql.Data.MySqlClient;

namespace Cumulative1.Model
{
    public class SchoolDbContext
    {
        private static string User { get { return "root"; } }
        private static string Password { get { return ""; } } 
        private static string Database { get { return "schooldb"; } }
        private static string Server { get { return "localhost"; } }
        private static string Port { get { return "3306"; } } 

        protected static string ConnectionString
        {
            get
            {
                
                return "server = " + Server
                  + "; user = " + User
                   + "; database = " + Database
                  + "; port = " + Port
                  + "; password = " + Password
                   + "; convert zero datetime = True";
            }
        }

        /// <summary>
        /// Returns a connection to the school database.
        /// </summary>
        /// <returns>A MySqlConnection object to interact with the database.</returns>
        public MySqlConnection AccessDatabase()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}
