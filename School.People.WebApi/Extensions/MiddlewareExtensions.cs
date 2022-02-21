using Microsoft.AspNetCore.Builder;
using School.People.WebApi.Middlewares;

namespace School.People.WebApi.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseUserValidator(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserValidator>();
        }

        public static IApplicationBuilder UseIdMapper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PersonIdMapper>();
        }
    }
}
