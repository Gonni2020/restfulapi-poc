using Microsoft.AspNetCore.Builder;

namespace RestfulAPI
{
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandler(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware.ExceptionMiddleware>();
        }
    }
}
