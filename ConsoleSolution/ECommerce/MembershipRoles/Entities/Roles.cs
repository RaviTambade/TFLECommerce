
namespace Transflower.ECommerce.Entities;

public class Roles
{
    public int Id { get; set; }

    public string Title { get; set; }

    public override string ToString()
    {
        return (""+Id+" "+Title);
    }

    internal void Add(Roles role)
    {
        throw new NotImplementedException();
    }
}

