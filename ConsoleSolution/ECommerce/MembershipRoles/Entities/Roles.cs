
namespace Transflower.ECommerce.Entities;

public class Role
{
    public int Id { get; set; }

    public string Title { get; set; }

    public override string ToString()
    {
        return (""+Id+" "+Title);
    }
}

