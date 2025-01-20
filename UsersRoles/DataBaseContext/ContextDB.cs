using Microsoft.EntityFrameworkCore;
using UsersRoles.Model;

namespace UsersRoles.DataBaseContext
{
    public class ContextDB : DbContext
    {
        public ContextDB(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Emails> Emails { get; set; }
        public DbSet<Roles> Roles { get; set; }
    }
}
