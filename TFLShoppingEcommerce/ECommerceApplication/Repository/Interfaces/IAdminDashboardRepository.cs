using ECommerceApplication.ViewModels;

namespace ECommerceApplication.Repository.Interfaces;

public interface IAdminDashboardRepository
{
    DashboardViewModel GetDashboard();
}

