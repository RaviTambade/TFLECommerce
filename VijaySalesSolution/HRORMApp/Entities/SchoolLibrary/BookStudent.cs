namespace HRORMApp.Entities.SchoolLibrary
{
    public  class BookStudent
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}