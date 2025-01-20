using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using NewsBlog.Web.Data;
using NewsBlog.Web.Models.Domain;

namespace NewsBlog.Web.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly NewsBlogDbContext newsBlogDbContext;

        public BlogPostRepository(NewsBlogDbContext newsBlogDbContext)
        {
            this.newsBlogDbContext = newsBlogDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await newsBlogDbContext.AddAsync(blogPost);
            await newsBlogDbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlog = await newsBlogDbContext.BlogPosts.FindAsync(id);
            if (existingBlog != null)
            {
                newsBlogDbContext.BlogPosts.Remove(existingBlog);
                await newsBlogDbContext.SaveChangesAsync();
                return existingBlog;
            }
            return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await newsBlogDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
            return await newsBlogDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            return await newsBlogDbContext.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            var existingBlog = await newsBlogDbContext.BlogPosts.Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingBlog != null)
            {
                existingBlog.Id = blogPost.Id;
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.Content = blogPost.Content;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlog.UrlHandle = blogPost.UrlHandle;
                existingBlog.Visible = blogPost.Visible;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.Tags = blogPost.Tags;

                await newsBlogDbContext.SaveChangesAsync();
                return existingBlog;
            }

            return null;
        }
    }
}
