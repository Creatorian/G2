using Gnome.Domain.Interfaces;
using Gnome.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Gnome.Infrastructure.Repositories
{
    public class AdminUserRepository : IAdminUserRepository
    {
        private readonly AppDbContext _context;

        public AdminUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<AdminUser> GetByUsernameAsync(string username)
        {
            return await _context.AdminUsers
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<AdminUser> GetByIdAsync(int id)
        {
            return await _context.AdminUsers
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<AdminUser> CreateAsync(AdminUser adminUser)
        {
            _context.AdminUsers.Add(adminUser);
            await _context.SaveChangesAsync();
            return adminUser;
        }

        public async Task<AdminUser> UpdateAsync(AdminUser adminUser)
        {
            _context.AdminUsers.Update(adminUser);
            await _context.SaveChangesAsync();
            return adminUser;
        }

        public async Task<bool> UpdateLastLoginAsync(int id)
        {
            var adminUser = await _context.AdminUsers.FindAsync(id);
            if (adminUser == null)
                return false;

            adminUser.LastLoginDateTime = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 