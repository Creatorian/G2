using System;

namespace Gnome.Api.Models.SwaggerResponses
{
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