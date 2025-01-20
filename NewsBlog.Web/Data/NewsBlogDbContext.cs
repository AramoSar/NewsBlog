using Microsoft.EntityFrameworkCore;
using NewsBlog.Web.Models.Domain;

namespace NewsBlog.Web.Data
{
    public class NewsBlogDbContext : DbContext
    {
        public NewsBlogDbContext(DbContextOptions<NewsBlogDbContext> options) : base(options)
        {
            
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
