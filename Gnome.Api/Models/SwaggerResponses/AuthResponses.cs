using System;

namespace Gnome.Api.Models.SwaggerResponses
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

    /// <summary>
    /// Response DTO for token validation
    /// </summary>
    public class TokenValidationResponse
    {
        /// <summary>
        /// Confirmation message that the token is valid
        /// </summary>
        /// <example>Token is valid</example>
        public string Message { get; set; } = string.Empty;
    }

    /// <summary>
    /// Response DTO for error responses
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Error message describing what went wrong
        /// </summary>
        /// <example>Invalid username or password</example>
        public string Message { get; set; } = string.Empty;
    }
} 