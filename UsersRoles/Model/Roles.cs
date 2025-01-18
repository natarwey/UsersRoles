using System.ComponentModel.DataAnnotations;

namespace UsersRoles.Model
{
    public class Roles
    {
        [Key]
        public int id_Role { get; set; }
        public string Tittle { get; set; }
    }
}
