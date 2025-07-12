using System;

namespace Gnome.Domain.DTOs
{
    /// <summary>
    /// DTO for refresh token requests
    /// </summary>
    public class RefreshTokenDto
    {
        /// <summary>
        /// The refresh token to use for generating new JWT token
        /// </summary>
        /// <example>eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...</example>
        public string RefreshToken { get; set; } = string.Empty;
    }
} 