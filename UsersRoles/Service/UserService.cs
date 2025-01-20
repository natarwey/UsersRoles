using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersRoles.DataBaseContext;
using UsersRoles.Interfaces;
using UsersRoles.Model;
using UsersRoles.Requests;

namespace UsersRoles.Service
{
    public class UserService : IUsersService
    {
        private readonly ContextDB _context;

        public UserService(ContextDB context)
        {
            _context = context;
        }

        public async Task<IActionResult> CreateNewUserAsync(CreateNewUser data)
        {
            //проверка на существование такой же роли
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Tittle == data.Tittle);
            if (role == null)
            {
                role = new Roles()
                {
                    Tittle = data.Tittle,
                };

                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();
            }

            var user = new Users()
            {
                Role_id = role.id_Role,
                Name = data.Name,
                Description = data.Description
            };

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var email = new Emails()
            {
                User_id = user.id_User,
                Email = data.Email,
                Password = data.Password,
            };

            await _context.Emails.AddAsync(email);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true
            });
        }

        public async Task<IActionResult> GetAllUsersAsync()
        {
            var users = await _context.Users
                //.Include(u => u.Roles) 
                //.Include(u => u.Emails)
                .ToListAsync();

            return new OkObjectResult(new
            {
                data = users,
                status = true
            });
        }
    }
}
