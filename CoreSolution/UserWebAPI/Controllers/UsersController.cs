using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserWebAPI.Entities;
using UserWebAPI.Services.Interfaces;

namespace UserWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _svc;

        public UsersController(IUserService service)
        {
            _svc = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _svc.GetAllUsers();
            return Ok(users);
        }
    }
}
