using KhersonNews.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace KhersonNews.DAL.Impl.PostgreSQL
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        DbSet<User> Users { get; set; }
        DbSet<UserRole> UserRoles { get; set; }

        DbSet<Post> Posts { get; set; }

        DbSet<Tag> Tags { get; set; }
        DbSet<PostTag> PostTags { get; set; }

        DbSet<Rubric> Rubrics { get; set; }
    }
}
