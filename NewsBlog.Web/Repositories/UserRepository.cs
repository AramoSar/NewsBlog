using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NewsBlog.Web.Data;

namespace NewsBlog.Web.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly NewsBlogDbContext newsBlogDbContext;

        public UserRepository(NewsBlogDbContext newsBlogDbContext)
        {
            this.newsBlogDbContext = newsBlogDbContext;
        }
        public async Task<IEnumerable<IdentityUser>> GetAll()
        {
            var users = await newsBlogDbContext.Users.ToListAsync();

            var superAdminUser = await newsBlogDbContext.Users
                .FirstOrDefaultAsync(x => x.Email == "superadmin@bloggie.com");

            if (superAdminUser is not null)
            {
                users.Remove(superAdminUser);
            }

            return users;

        }
    }
}
