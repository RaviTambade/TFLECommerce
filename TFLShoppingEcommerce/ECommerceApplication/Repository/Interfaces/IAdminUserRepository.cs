using ECommerceApplication.ViewModels;

namespace ECommerceApplication.Repository.Interfaces;

public interface IAdminUserRepository
{
    List<AdminUserListItem> GetAllUsersWithStats();
    AdminUserDetailsViewModel? GetUserDetails(int userId);
    bool DeleteUser(int userId);
}

