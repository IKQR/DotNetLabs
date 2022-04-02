using System.ComponentModel.DataAnnotations.Schema;

namespace KhersonNews.DAL.Entities
{
    public class PostTag
    {
        [ForeignKey(nameof(Post))]
        public int PostId { get; set; }
        public Post Post { get; set; }

        [ForeignKey(nameof(Tag))]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
