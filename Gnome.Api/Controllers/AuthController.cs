using Gnome.Domain.DTOs;
using Gnome.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace Gnome.Api.Controllers
{
    /// <summary>
    /// Authentication controller for admin user login, token refresh, and validation
    /// </summary>
    [ApiController]
    [Route("auth")]
    [SwaggerTag("Authentication operations for admin users")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Authenticates an admin user and returns JWT and refresh tokens
        /// </summary>
        /// <param name="loginDto">Login credentials containing username and password</param>
        /// <returns>JWT token, refresh token, expiration time, and user information</returns>
        /// <response code="200">Login successful. Returns authentication tokens and user data.</response>
        /// <response code="401">Invalid username or password.</response>
        /// <response code="500">Internal server error during authentication.</response>
        [HttpPost("login")]
        [SwaggerOperation(Summary = "Admin user login", Description = "Authenticates admin user credentials and returns JWT and refresh tokens")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var result = await _authService.LoginAsync(loginDto);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning("Login failed: {Message}", ex.Message);
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Refreshes JWT token using a valid refresh token
        /// </summary>
        /// <param name="refreshTokenDto">Refresh token data</param>
        /// <returns>New JWT token, new refresh token, and updated expiration time</returns>
        /// <response code="200">Token refresh successful. Returns new authentication tokens.</response>
        /// <response code="401">Invalid or expired refresh token.</response>
        /// <response code="500">Internal server error during token refresh.</response>
        [HttpPost("refresh")]
        [SwaggerOperation(Summary = "Refresh JWT token", Description = "Generates new JWT and refresh tokens using a valid refresh token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        {
            try
            {
                var result = await _authService.RefreshTokenAsync(refreshTokenDto.RefreshToken);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning("Token refresh failed: {Message}", ex.Message);
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Validates the current JWT token
        /// </summary>
        /// <returns>Confirmation that the token is valid</returns>
        /// <response code="200">Token is valid and user is authenticated.</response>
        /// <response code="401">Token is invalid or expired.</response>
        [HttpGet("validate")]
        [Authorize]
        [SwaggerOperation(Summary = "Validate JWT token", Description = "Validates the current JWT token and confirms user authentication status")]
        public async Task<IActionResult> ValidateToken()
        {
            return Ok(new { message = "Token is valid" });
        }
    }
} 