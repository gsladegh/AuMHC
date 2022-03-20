using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ADO_Example
{
    class DataAccessLayer
    {
        // database connection string
        private string _connectionstring;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectionstring">Database connection string</param>
        public DataAccessLayer(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        /// <summary>
        /// Inserts a person record into the database
        /// </summary>
        /// <param name="p">Person object</param>
        public void insertPerson(Person p)
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_InsertPerson", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = p.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = p.LastName;
                    cmd.Parameters.Add("@DOB", SqlDbType.VarChar).Value = p.DOB;

                    cmd.ExecuteNonQuery();
                }                
            }
        }

        /// <summary>
        /// Gets people records from the database, adds them to list of people.
        /// Now that I've done it this way, thinking about performance, I might not 
        /// abstract out the Person object and add it to a list, as it's not really necessary
        /// to complete the task and with a greater number of records will start to have
        /// an impact.
        /// </summary>
        /// <returns>List<Person> object of people from the dbo.People table</Person></returns>
        public List<Person> getPeople()
        {
            using (SqlConnection conn = new SqlConnection(_connectionstring))
            {
                List<Person> people = new List<Person>();
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("sp_getPeople", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            people.Add(new Person(reader["FirstName"].ToString(),reader["LastName"].ToString(),reader["DOB"].ToString()));
                        }
                    }
                }
                return people;
            }
        }
    }
}
