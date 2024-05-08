using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transflower.Entities;

namespace Transflower.Helpers
{
    public class HRManager
    {
        public static Person GetPerson(int id)
        {
            var person = new Person
            {
                Id = id,
                FirstName = "Manisha",
                LastName = "Pant",
                Contact = "9887347854",
                Email = "manisha.pant@gmail.com"
            };
            return person;
        }
    }

}
