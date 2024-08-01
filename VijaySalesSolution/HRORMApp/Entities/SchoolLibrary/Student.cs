namespace HRORMApp.Entities.SchoolLibrary
{
    public  class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public List<BookStudent> BookStudents { get; set; }
    }
}
