using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Entities
{

    //Entity
    //Assembly Scope
    internal class Person
    {
        //access specifiers
        //defines visibility of variable
        //public , private, protected, internal

        //data members

        private string _firstName;
        private string _lastName;
        private string _email;
        private bool _married;
        private int _age;





        //Member functions


        //Single Responsibility Principle

        //Each member function will have unique Responsibility
        //Default constructor
        public Person()
        {

            //Write behaviour for  object Initialization
            _firstName = "Sachin";
            _lastName = "Sonawane";
            _email = "sachin.sononwane@gmail.com";
            _married = false;
            _age = 24;
        }

        public override string ToString()
        {
            //Write behaviour for ToSTring method
            //Method implement
            //Logic
            return "*******" + _firstName + " " + _lastName + " " + _email;
        }
    }
}
