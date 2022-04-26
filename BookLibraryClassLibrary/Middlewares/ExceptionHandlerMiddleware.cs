using BookLibraryClassLibrary.CustomExceptions;
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
            catch (CustomExceptionsBase cex)
            {
                await HanldeException(context, cex);
            }
            catch (Exception ex)
            {
                await HanldeException(context, ex);
            }
        }

        private static Task HanldeException(HttpContext context, Exception ex)
        {
            if (ex is CustomExceptionsBase)
            {
                context.Response.StatusCode = (ex as CustomExceptionsBase).StatusCode;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.Response.ContentType = "application/json";

            return context.Response.WriteAsJsonAsync(new ErrorVM
            {
                Message = $"Unexpected Error Occured, Request Failed; {ex.Message}",
                StatusCode = context.Response.StatusCode
            });
        }
    }
}
