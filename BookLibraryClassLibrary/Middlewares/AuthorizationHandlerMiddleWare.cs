using BookLibraryClassLibrary.Structs;
using BookLibraryClassLibrary.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Middlewares
{
    public class AuthorizationHandlerMiddleWare /*: IMiddleware */
    {
        private readonly RequestDelegate _next;

        public AuthorizationHandlerMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (NoAuthRoutes.Routes.Contains(context.Request.Path.ToString().ToLower()))
            {
                await _next(context);
            }
            else
            {
                if (!context.Request.Headers.ContainsKey("Authorization"))
                {
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsJsonAsync(new ErrorVM
                    {
                        Message = $"Authorization token not found in request's header; path: {context.Request.Path}",
                        StatusCode = 401
                    });
                }
                else
                {
                    await _next(context);
                }
            }
        }
    }
}
