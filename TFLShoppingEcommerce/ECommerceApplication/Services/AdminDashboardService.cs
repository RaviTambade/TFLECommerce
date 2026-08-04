using ECommerceApplication.Repository.Interfaces;
using ECommerceApplication.Services.Interfaces;
using ECommerceApplication.ViewModels;

namespace ECommerceApplication.Services;

public class AdminDashboardService : IAdminDashboardService
{
    private readonly IAdminDashboardRepository _repo;

    public AdminDashboardService(IAdminDashboardRepository repo)
    {
        _repo = repo;
    }

    public DashboardViewModel GetDashboard() => _repo.GetDashboard();
}

