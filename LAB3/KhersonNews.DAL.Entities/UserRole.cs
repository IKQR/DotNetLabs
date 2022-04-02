using System.ComponentModel.DataAnnotations;

namespace KhersonNews.DAL.Entities
{
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}