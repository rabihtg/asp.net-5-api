using BookLibraryClassLibrary.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HanldeException(context, ex);
            }
        }

        private static Task HanldeException(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = ((int)HttpStatusCode.InternalServerError);
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsJsonAsync(new ErrorVM
            {
                Message = $"Unexpected Error Occured, Request Failed; {ex.Message}",
                StatusCode = 500
            });
        }
    }
}
