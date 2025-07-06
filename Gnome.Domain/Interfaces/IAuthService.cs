using Gnome.Domain.DTOs;
using System.Threading.Tasks;

namespace Gnome.Domain.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        Task<LoginResponseDto> RefreshTokenAsync(string refreshToken);
        Task<bool> ValidateTokenAsync(string token);
    }
} 