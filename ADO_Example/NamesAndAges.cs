using System;
using System.Collections.Generic;
using System.Text;

namespace ADO_Example
{
    /// <summary>
    /// Class representing a collection of people (Person objects)
    /// </summary>
    class NamesAndAges
    {
        // A collection of people
        public List<Person> PersonList;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public NamesAndAges()
        {
            PersonList = new List<Person>();
        }
        
        /// <summary>
        /// Method to add person to PersonList
        /// </summary>
        /// <param name="person">Person object</param>
        public void addPerson(Person person)
        {
            PersonList.Add(person);
        }
    }
}
