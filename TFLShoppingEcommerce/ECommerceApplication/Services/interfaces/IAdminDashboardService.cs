using ECommerceApplication.ViewModels;

namespace ECommerceApplication.Services.Interfaces;

public interface IAdminDashboardService
{
    DashboardViewModel GetDashboard();
}

