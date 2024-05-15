using TestDapperApp.Entities;
using TestDapperApp.Repositories.Interfaces;

namespace TestDapperApp.Repositories.EntityFramemwork
{
    public  class StudentRepository:IStudentRepository
    {
        //Using Stored Procedure
        public   Student GetById(int id)
        {
            var student=new Student();
   
            return student;
        }
        public   bool InsertParamExecute(Student student)
        {
            //DML Operations
            bool status = false;
            
            return status;
        }
        public    bool Insert(Student student)
        {
            //Basic Code

            bool status = false;
           
            return status;
        }
        public    bool Delete(Student student)
        {
                bool status =false; 
            
            return status;
        }
        public    bool Update(Student student)
        {
           bool status = false;
            
           return status;
        
        }
        public   Student GetStudentById(int id)
        {
            Student std = new Student(); ;
            
            return std;
        }
    }
}

