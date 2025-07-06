using Gnome.Domain.Models;
using System.Threading.Tasks;

namespace Gnome.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetByTokenAsync(string token);
        Task<RefreshToken> CreateAsync(RefreshToken refreshToken);
        Task<RefreshToken> UpdateAsync(RefreshToken refreshToken);
        Task<bool> RevokeTokenAsync(string token);
        Task<bool> RevokeAllTokensForUserAsync(int adminUserId);
    }
} 