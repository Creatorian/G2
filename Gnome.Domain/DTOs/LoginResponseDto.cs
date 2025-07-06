namespace Gnome.Domain.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresAt { get; set; }
        public AdminUserDto User { get; set; }
    }
} 