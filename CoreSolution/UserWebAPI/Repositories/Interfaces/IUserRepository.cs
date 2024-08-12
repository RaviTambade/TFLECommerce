using System.Collections.Generic;
using System.Threading.Tasks;
using UserWebAPI.Entities;

namespace UserWebAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
    }
}
