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
            // Проверка на уникальность email
            var emailExists = await _context.Emails.AnyAsync(e => e.Email == data.Email);
            if (emailExists)
            {
                return new BadRequestObjectResult(new
                {
                    status = false,
                    message = "Такой email уже существует. Введите другой email"
                });
            }

            var role = 2;

            var user = new Users()
            {
                Role_id = 2,
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

        public async Task<IActionResult> UpdateUserAsync(int userId, UpdateUserRequest data)
        {
            var user = await _context.Users
                .Include(u => u.Emails)
                .FirstOrDefaultAsync(u => u.id_User == userId);

            if (user == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Пользователь не найден"
                });
            }

            if (!string.IsNullOrEmpty(data.Name))
            {
                user.Name = data.Name;
            }

            if (!string.IsNullOrEmpty(data.Description))
            {
                user.Description = data.Description;
            }

            if (!string.IsNullOrEmpty(data.Email))
            {
                var email = user.Emails.FirstOrDefault();
                if (email != null)
                {
                    // Проверяем, не занят ли новый email другим пользователем
                    var emailExists = await _context.Emails
                        .AnyAsync(e => e.Email == data.Email && e.User_id != userId);

                    if (emailExists)
                    {
                        return new BadRequestObjectResult(new
                        {
                            status = false,
                            message = "Такой email уже существует. Введите другой email"
                        });
                    }

                    email.Email = data.Email;
                }
            }

            if (!string.IsNullOrEmpty(data.Password))
            {
                var email = user.Emails.FirstOrDefault();
                if (email != null)
                {
                    email.Password = data.Password;
                }
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Пользователь успешно обновлен"
            });
        }

        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            var user = await _context.Users
                .Include(u => u.Emails)
                .FirstOrDefaultAsync(u => u.id_User == userId);

            if (user == null)
            {
                return new NotFoundObjectResult(new
                {
                    status = false,
                    message = "Пользователь не найден"
                });
            }

            var emails = user.Emails.ToList();
            _context.Emails.RemoveRange(emails);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return new OkObjectResult(new
            {
                status = true,
                message = "Пользователь успешно удален"
            });
        }
    }
}
