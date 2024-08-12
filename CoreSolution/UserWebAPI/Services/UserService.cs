using System.Collections.Generic;
using System.Threading.Tasks;
using UserWebAPI.Entities;
using UserWebAPI.Repositories.Interfaces;
using UserWebAPI.Services.Interfaces;

namespace UserWebAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _repository.GetAllUsers();
        }
    }
}
