using MarketApp.Models;
using Microsoft.AspNetCore.Mvc;
using MarketApp.DataAccess.Repositories;

namespace WebApplication1.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        UsersDataAccess _usersDataAccess;

        public UserController(UsersDataAccess usersDataAccess)
        {
            _usersDataAccess = usersDataAccess;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<User> users = await _usersDataAccess.GetAllUsersAsync();
            return Ok(users);
        }
        
    }
}
