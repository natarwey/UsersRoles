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

        [HttpPut]
        [Route("updateUser/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, UpdateUserRequest data)
        {
            return await _userService.UpdateUserAsync(userId, data);
        }

        [HttpDelete]
        [Route("deleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            return await _userService.DeleteUserAsync(userId);
        }
    }
}
