using Microsoft.AspNetCore.Identity;

namespace NewsBlog.Web.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<IdentityUser>> GetAll();
    }
}
