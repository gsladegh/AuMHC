using System;
using System.Collections.Generic;
using System.Text;

namespace ADO_Example
{
    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DOB { get; set; }

        public Person(string fName, string lName, string bday)
        {
            FirstName = fName;
            LastName = lName;
            DOB = bday;
        }
    }
}
