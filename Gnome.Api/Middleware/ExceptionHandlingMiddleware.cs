using Gnome.Application.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Gnome.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            object response;

            switch (exception)
            {
                case ValidationErrorException validationEx:
                    context.Response.StatusCode = validationEx.HttpStatusCode;
                    response = new
                    {
                        error = new
                        {
                            message = "Validation error occurred.",
                            details = validationEx.ValidationError.Message,
                            validationErrors = new[] { validationEx.ValidationError },
                            timestamp = DateTime.UtcNow,
                            traceId = context.TraceIdentifier
                        }
                    };
                    break;

                case InvalidOperationException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new
                    {
                        error = new
                        {
                            message = "Invalid operation.",
                            details = exception.Message,
                            timestamp = DateTime.UtcNow,
                            traceId = context.TraceIdentifier
                        }
                    };
                    break;

                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response = new
                    {
                        error = new
                        {
                            message = "Unauthorized access.",
                            details = exception.Message,
                            timestamp = DateTime.UtcNow,
                            traceId = context.TraceIdentifier
                        }
                    };
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response = new
                    {
                        error = new
                        {
                            message = "An error occurred while processing your request.",
                            details = exception.Message,
                            timestamp = DateTime.UtcNow,
                            traceId = context.TraceIdentifier
                        }
                    };
                    break;
            }

            var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            await context.Response.WriteAsync(jsonResponse);
        }
    }
} 