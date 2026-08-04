using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Services.Interfaces;
using ECommerceApplication.ViewModels;

namespace ECommerceApplication.Services;

public class AdminUserService : IAdminUserService
{
    private readonly IAdminUserRepository _repo;

    public AdminUserService(IAdminUserRepository repo)
    {
        _repo = repo;
    }

    public List<AdminUserListItem> GetAllUsersWithStats() => _repo.GetAllUsersWithStats();

    public AdminUserDetailsViewModel? GetUserDetails(int userId) => _repo.GetUserDetails(userId);

    public bool DeleteUser(int userId) => _repo.DeleteUser(userId);
}

