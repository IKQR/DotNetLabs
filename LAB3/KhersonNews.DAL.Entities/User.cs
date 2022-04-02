using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhersonNews.DAL.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }

        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }
        public UserRole Role { get; set; }
    }
}
