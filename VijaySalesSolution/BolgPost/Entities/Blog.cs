
namespace BolgPost.Entities
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Catagory { get; set; }
        public List <Post> Posts { get; set; }
    }
}
