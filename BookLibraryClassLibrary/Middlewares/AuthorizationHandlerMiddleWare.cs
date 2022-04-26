using BookLibraryClassLibrary.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BookLibraryClassLibrary.Middlewares
{
    public class AuthorizationHandlerMiddleWare
    {
        private readonly RequestDelegate _next;

        public AuthorizationHandlerMiddleWare(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new ErrorVM
                {
                    Message = "Authorization token not found in request's header.",
                    StatusCode = 401
                });
            }
            else
            {
                await _next(context);
                if (context.Response.StatusCode == 401)
                {
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsJsonAsync(new ErrorVM
                    {
                        Message = "Authorization Failed.",
                        StatusCode = 401
                    });
                }
            }
        }
    }
}
