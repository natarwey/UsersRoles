using Microsoft.AspNetCore.Mvc;
using UsersRoles.Requests;

namespace UsersRoles.Interfaces
{
    public interface IUsersService
    {
        Task<IActionResult> GetAllUsersAsync();
        Task<IActionResult> CreateNewUserAsync(CreateNewUser data);
    }
}
