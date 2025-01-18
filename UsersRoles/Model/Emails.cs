using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace UsersRoles.Model
{
    public class Emails
    {
        [Key]
        public int id_Email { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int User_id { get; set; }
        public Users Users { get; set; }
    }
}
