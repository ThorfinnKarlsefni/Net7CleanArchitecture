using System;
using FluentValidation;

namespace Logistics.WebApi.Middleware
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                var errorMessage = ex.Errors.First().ErrorMessage;
                context.Response.StatusCode = 400;
                context.Response.ContentType = "text/plain";

                await context.Response.WriteAsync(errorMessage);
            }
        }
    }
}

