using HR.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Operations
{
    internal class HRManager
    {
        public HRManager() { }

        //CRUD Operation
        public List<Person> GetAll()
        {
            List<Person> list = new List<Person>();
            return list;
        }
        public bool Insert()
        {
            return false;
        }
        public bool Update()
        {
            return false;
        }
        public bool Remove()
        {
            return false;
        }
    }
}
