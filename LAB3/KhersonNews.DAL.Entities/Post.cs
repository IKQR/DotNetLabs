using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhersonNews.DAL.Entities
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

        [Required]
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset? EditedTime { get; set; }

        [Required]
        public PostState State { get; set; }

        [Required]
        [ForeignKey(nameof(Rubric))]
        public int RubricId { get; set; }
        public Rubric Rubric { get; set; }

        [Required]
        [ForeignKey(nameof(Author))]
        public int AuthorId { get; set; }
        public User Author { get; set; }

        [ForeignKey(nameof(Editor))]
        public int? EditorId { get; set; }
        public User Editor { get; set; }
    }

    public enum PostState
    {
        Created,
        Published,
        Removed,
    }
}
