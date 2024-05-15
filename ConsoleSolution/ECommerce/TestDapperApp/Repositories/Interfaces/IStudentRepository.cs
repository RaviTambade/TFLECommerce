
using TestDapperApp.Entities;

namespace TestDapperApp.Repositories.Interfaces
{
    internal interface IStudentRepository
    {
        public Student GetById(int id);
            public   bool InsertParamExecute(Student student);
            public   bool Insert(Student student);
            public   bool Delete(Student student);           
            public   bool Update(Student student);

            public  Student GetStudentById(int id);  
    }
}

