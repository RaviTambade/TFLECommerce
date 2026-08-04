using ECommerceApplication.Filters;
using ECommerceApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApplication.Controllers;

[AdminOnly]
public class AdminDashboardController : Controller
{
    private readonly IAdminDashboardService _service;

    public AdminDashboardController(IAdminDashboardService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var vm = _service.GetDashboard();
        return View(vm);
    }

    // Real-time polling endpoint (AJAX)
    [HttpGet]
    public IActionResult GetDashboardStats()
    {
        var vm = _service.GetDashboard();
        return Json(vm);
    }
}

