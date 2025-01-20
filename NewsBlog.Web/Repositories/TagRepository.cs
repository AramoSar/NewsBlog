using Microsoft.EntityFrameworkCore;
using NewsBlog.Web.Data;
using NewsBlog.Web.Models.Domain;
using NewsBlog.Web.Models.ViewModels;

namespace NewsBlog.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly NewsBlogDbContext newsBlogDbContext;

        public TagRepository(NewsBlogDbContext newsBlogDbContext)
        {
            this.newsBlogDbContext = newsBlogDbContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await newsBlogDbContext.Tags.AddAsync(tag);
            await newsBlogDbContext.SaveChangesAsync();

            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await newsBlogDbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                var removedTag = newsBlogDbContext.Tags.Remove(existingTag);
                await newsBlogDbContext.SaveChangesAsync();
                return existingTag;
            }

            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await newsBlogDbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            return await newsBlogDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await newsBlogDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null) 
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                newsBlogDbContext.SaveChanges();
                return existingTag;
            }
            return null;
        }
    }
}
