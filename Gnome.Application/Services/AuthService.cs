using Gnome.Domain.DTOs;
using Gnome.Domain.Interfaces;
using Gnome.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gnome.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAdminUserRepository _adminUserRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly JwtService _jwtService;
        private readonly ILogger<AuthService> _logger;
        private readonly IConfiguration _configuration;

        public AuthService(
            IAdminUserRepository adminUserRepository,
            IRefreshTokenRepository refreshTokenRepository,
            JwtService jwtService,
            ILogger<AuthService> logger,
            IConfiguration configuration)
        {
            _adminUserRepository = adminUserRepository;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtService = jwtService;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            var adminUser = await _adminUserRepository.GetByUsernameAsync(loginDto.Username);

            var testHash = AuthService.HashPassword(loginDto.Password);
            Console.WriteLine("Test Hash here: " + testHash);


            if (adminUser == null || !adminUser.IsActive)
            {
                _logger.LogWarning("Login attempt failed for username: {Username} - User not found or inactive", loginDto.Username);
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            if (!VerifyPassword(loginDto.Password, adminUser.PasswordHash))
            {
                _logger.LogWarning("Login attempt failed for username: {Username} - Invalid password", loginDto.Username);
                throw new UnauthorizedAccessException("Invalid username or password");
            }

            await _adminUserRepository.UpdateLastLoginAsync(adminUser.Id);

            // Generate tokens
            var token = _jwtService.GenerateJwtToken(adminUser.Id, adminUser.Username, adminUser.Email);
            var refreshToken = _jwtService.GenerateRefreshToken();

            var refreshTokenEntity = new RefreshToken
            {
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                AdminUserId = adminUser.Id
            };

            await _refreshTokenRepository.CreateAsync(refreshTokenEntity);

            _logger.LogInformation("Successful login for user: {Username}", adminUser.Username);

            return new LoginResponseDto
            {
                Token = token,
                RefreshToken = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddHours(24), // 24 hours
                User = new AdminUserDto
                {
                    Id = adminUser.Id,
                    Username = adminUser.Username,
                    Email = adminUser.Email,
                    FirstName = adminUser.FirstName,
                    LastName = adminUser.LastName,
                    IsActive = adminUser.IsActive,
                    CreatedDateTime = adminUser.CreatedDateTime,
                    LastLoginDateTime = adminUser.LastLoginDateTime
                }
            };
        }

        public async Task<LoginResponseDto> RefreshTokenAsync(string refreshToken)
        {
            // Get refresh token from database
            var refreshTokenEntity = await _refreshTokenRepository.GetByTokenAsync(refreshToken);
            
            if (refreshTokenEntity == null)
            {
                _logger.LogWarning("Refresh token not found or expired: {Token}", refreshToken);
                throw new UnauthorizedAccessException("Invalid refresh token");
            }

            // Get the admin user
            var adminUser = refreshTokenEntity.AdminUser;
            if (adminUser == null || !adminUser.IsActive)
            {
                _logger.LogWarning("Admin user not found or inactive for refresh token: {Token}", refreshToken);
                throw new UnauthorizedAccessException("Invalid refresh token");
            }

            // Generate new tokens
            var newToken = _jwtService.GenerateJwtToken(adminUser.Id, adminUser.Username, adminUser.Email);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            // Revoke the old refresh token
            await _refreshTokenRepository.RevokeTokenAsync(refreshToken);

            // Store new refresh token
            var newRefreshTokenEntity = new RefreshToken
            {
                Token = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7), // 7 days
                AdminUserId = adminUser.Id
            };

            await _refreshTokenRepository.CreateAsync(newRefreshTokenEntity);

            _logger.LogInformation("Token refreshed successfully for user: {Username}", adminUser.Username);

            return new LoginResponseDto
            {
                Token = newToken,
                RefreshToken = newRefreshToken,
                ExpiresAt = DateTime.UtcNow.AddHours(24), // 24 hours
                User = new AdminUserDto
                {
                    Id = adminUser.Id,
                    Username = adminUser.Username,
                    Email = adminUser.Email,
                    FirstName = adminUser.FirstName,
                    LastName = adminUser.LastName,
                    IsActive = adminUser.IsActive,
                    CreatedDateTime = adminUser.CreatedDateTime,
                    LastLoginDateTime = adminUser.LastLoginDateTime
                }
            };
        }

        private bool VerifyPassword(string password, string passwordHash)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashedPassword = Convert.ToBase64String(hashedBytes);
            return hashedPassword == passwordHash;
        }

        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
} 