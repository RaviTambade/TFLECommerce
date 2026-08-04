using ECommerceApplication.ViewModels;

namespace ECommerceApplication.Services.Interfaces;

public interface IAdminUserService
{
    List<AdminUserListItem> GetAllUsersWithStats();
    AdminUserDetailsViewModel? GetUserDetails(int userId);
    bool DeleteUser(int userId);
}

