using Microsoft.AspNetCore.Mvc;
using UsersRoles.Requests;

namespace UsersRoles.Interfaces
{
    public interface IUsersService
    {
        Task<IActionResult> GetAllUsersAsync();
        Task<IActionResult> CreateNewUserAsync(CreateNewUser data);
        Task<IActionResult> UpdateUserAsync(int userId, UpdateUserRequest data);
        Task<IActionResult> DeleteUserAsync(int userId);
    }
}
