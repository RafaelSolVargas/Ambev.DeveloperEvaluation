using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Common;
using FluentValidation;
using System.Text.Json;

namespace Ambev.DeveloperEvaluation.WebApi.Middleware
{
    public class ExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private static JsonSerializerOptions JsonSerializerOptions { get; set; } = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public ExceptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (InvalidOperationException ex)
            {
                await HandleInvalidOperationExceptionAsync(context, ex);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleKeyNotFoundExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                HandleDefaultExceptionAsync(context, ex);
            }
        }

        private static void HandleDefaultExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new ApiResponse
            {
                Success = false,
                Message = $"Internal Error: {exception.Message}",
                Errors = [ ]
            };

            context.Response.WriteAsync(JsonSerializer.Serialize(response, JsonSerializerOptions));
        }

        private static Task HandleInvalidOperationExceptionAsync(HttpContext context, InvalidOperationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new ApiResponse
            {
                Success = false,
                Message = "Invalid operation",
                Errors =
                [
                    new ValidationErrorDetail()
                    {
                        Detail = "Invalid operation",
                        Error = exception.Message
                    }
                ]
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, JsonSerializerOptions));
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new ApiResponse
            {
                Success = false,
                Message = "Validation Failed",
                Errors = exception.Errors
                    .Select(error => (ValidationErrorDetail)error)
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response, JsonSerializerOptions));
        }

        private static Task HandleKeyNotFoundExceptionAsync(HttpContext context, KeyNotFoundException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status404NotFound;

            var response = new ApiResponse
            {
                Success = false,
                Message = exception.Message,
                Errors =
                [
                    new ValidationErrorDetail()
                    {
                        Detail = "Invalid key used",
                        Error = exception.Message
                    }
                ]
            };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response, JsonSerializerOptions));
        }
    }
}
