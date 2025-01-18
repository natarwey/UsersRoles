using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UsersRoles.Model
{
    public class Users
    {
        [Key]
        public int id_User { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        [ForeignKey("Roles")]
        public int Role_id { get; set; }
        public Roles Roles { get; set; }
    }
}
