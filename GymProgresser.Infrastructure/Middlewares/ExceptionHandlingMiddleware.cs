using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;


namespace GymProgresser.Infrastructure.Middlewares
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
            catch (ValidationException ex) 
            {
                 var errors = ex.Errors
                        .GroupBy(e => e.PropertyName) 
                        .ToDictionary(
                            g => g.Key, 
                            g => g.Select(e => e.ErrorMessage).ToList() 
                        );

                var response = new
                {
                    status = "error",
                    errors = errors
                };

                context.Response.StatusCode = 400;  
                context.Response.ContentType = "application/json";  

                var jsonResponse = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(jsonResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");
                context.Response.StatusCode = 500; 
                await context.Response.WriteAsync("An internal server error occurred.");
            }
        }
    }
}
