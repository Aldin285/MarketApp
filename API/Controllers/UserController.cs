using MarketApp.Models;
using Microsoft.AspNetCore.Mvc;
using MarketApp.DataAccess.Repositories;

namespace WebApplication1.API.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    [Tags("Users")]
    public class UserController : ControllerBase
    {

        UsersDataAccess _usersDataAccess;

        public UserController(UsersDataAccess usersDataAccess)
        {
            _usersDataAccess = usersDataAccess;
        }

        /// <summary>
        /// Fetches all users from the database.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        // This line ensures that the endpoint is included in the Swagger documentation
        //Putting "true" will hide the endpoint from Swagger
        [ApiExplorerSettings(IgnoreApi = false)] 
        public async Task<IActionResult> Get()
        {
            List<User> users = await _usersDataAccess.GetAllUsersAsync();
            return Ok(users);
        }
        
    }
}
