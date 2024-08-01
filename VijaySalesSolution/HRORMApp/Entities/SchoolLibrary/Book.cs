namespace HRORMApp.Entities.SchoolLibrary
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
        public List<BookStudent> BookStudents { get; set; }
    }
}
