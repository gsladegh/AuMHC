using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Linq;

namespace ADO_Example
{
    /// <summary>
    /// .net core 3.1 console application that reads a file
    /// of comma delimited lines from a text file in the format 
    /// FirstName, LastName, DOB (representing a person). It
    /// skips the first line which is a header.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            DataAccessLayer dal = new DataAccessLayer(ConfigurationManager.ConnectionStrings["sqlconn"].ToString());
            addPeopleToDatabase(dal);
            writePeopleToConsole(dal);
        }

        /// <summary>
        /// Read file, create list of people, and add them to the database
        /// </summary>
        /// <param name="dal">reference to data access layer</param>
        static void addPeopleToDatabase(DataAccessLayer dal)
        {
            string fileName = GetFilePath();

            // read file of names, create each person and add to a list
            try
            {
                NamesAndAges peoplelist = new NamesAndAges();

                // skip the first line since it's a header
                foreach (string line in File.ReadLines(fileName).Skip(1))
                {
                    // handle empty lines at the end of the file
                    if (!String.IsNullOrEmpty(line))
                    {
                        Person p = CreatePerson(line);
                        peoplelist.addPerson(p);
                    }
                }

                // peoplelist.PersonList doesn't sound so good
                foreach (Person person in peoplelist.PersonList)
                {
                    dal.insertPerson(person);
                }
            }
            catch (FileNotFoundException fnfe)
            {
                Console.WriteLine("I didn't find the file, sorry: " + fnfe.StackTrace);                
            }
            catch(Exception e)
            {
                Console.WriteLine("Unknown Exception: " + e.StackTrace);
            }
        }

        /// <summary>
        /// Get data from People table and write them out to the console.
        /// Use .ToShortDateString() to remove timestamp
        /// </summary>
        /// <param name="dal">reference to data access layer</param>
        static void writePeopleToConsole(DataAccessLayer dal)
        {
            try
            {
                List<Person> people = new List<Person>();

                people = dal.getPeople();

                foreach (var person in people)
                {
                    Console.WriteLine(String.Format("{0},{1},{2}", person.FirstName, person.LastName, DateTime.Parse(person.DOB).ToShortDateString()));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown Exception: " + e.StackTrace);
            }
        }

        /// <summary>
        /// Creates a person object from a comma delimited line of text
        /// </summary>
        /// <param name="line">line of text (i.e. "John,Riggins,3/4/1944")</param>
        /// <returns>Person object</returns>
        static Person CreatePerson(string line)
        {
            Person p = null;

            try
            {
                string[] personarray = line.Split(',');
                if (personarray.Length != 3)
                    throw new ArgumentException();
                p = new Person(personarray[0], personarray[1], personarray[2]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown Exception: " + e.StackTrace);                
            }

            return p;
        }

        /// <summary>
        /// Gets the filepath from the solution which runs from subdirectory
        /// Is there a better way to do this?  I would think so ... 
        /// </summary>
        /// <returns></returns>
        static string GetFilePath()
        {
            string filePath = Directory.GetParent(Directory.GetParent(Directory.GetParent(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory)).FullName).FullName).FullName;
            return filePath + "\\NamesAndAges.txt";
        }

        /// <summary>
        /// helper method to just make sure I was getting connection string correctly
        /// </summary>
        static void GetConnectionStrings()
        {
            ConnectionStringSettingsCollection settings =
                ConfigurationManager.ConnectionStrings;

            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings)
                {
                    Console.WriteLine(cs.Name);
                    Console.WriteLine(cs.ProviderName);
                    Console.WriteLine(cs.ConnectionString);
                }
            }
        }
    }
}
