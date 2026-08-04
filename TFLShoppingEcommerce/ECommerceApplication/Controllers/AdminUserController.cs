using ECommerceApplication.Filters;
using ECommerceApplication.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceApplication.Controllers;

[AdminOnly]
public class AdminUserController : Controller
{
    private readonly IAdminUserService _service;

    public AdminUserController(IAdminUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var users = _service.GetAllUsersWithStats();
        return View(users);
    }

    [HttpGet]
    public IActionResult ViewDetails(int id)
    {
        var vm = _service.GetUserDetails(id);
        if (vm == null) return NotFound();
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _service.DeleteUser(id);
        return RedirectToAction(nameof(Index));
    }
}

