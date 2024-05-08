using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transflower.Entities
{

    public class Person
    {
        private int id;

        //Property
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        //Auto Property
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact {  get; set; }
        public string Email {  get; set; }

    }
}