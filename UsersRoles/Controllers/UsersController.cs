using Microsoft.AspNetCore.Mvc;
using UsersRoles.Interfaces;
using UsersRoles.Requests;
using UsersRoles.Service;

namespace UsersRoles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService usersService)
        {
            _userService = usersService;
        }

        [HttpGet]
        [Route("getAllUser")]
        public async Task<IActionResult> GetAllUsers()
        {
            return await _userService.GetAllUsersAsync();
        }

        [HttpPost]
        [Route("createNewUser")]
        public async Task<IActionResult> CreateNewUser(CreateNewUser data)
        {
            return await _userService.CreateNewUserAsync(data);
        }
    }
}
