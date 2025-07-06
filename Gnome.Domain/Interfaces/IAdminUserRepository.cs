using Gnome.Domain.Models;
using System.Threading.Tasks;

namespace Gnome.Domain.Interfaces
{
    public interface IAdminUserRepository
    {
        Task<AdminUser> GetByUsernameAsync(string username);
        Task<AdminUser> GetByIdAsync(int id);
        Task<AdminUser> CreateAsync(AdminUser adminUser);
        Task<AdminUser> UpdateAsync(AdminUser adminUser);
        Task<bool> UpdateLastLoginAsync(int id);
    }
} 