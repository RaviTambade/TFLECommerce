using System.Collections.Generic;
using System.Threading.Tasks;
using UserWebAPI.Entities;

namespace UserWebAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
    }
}
