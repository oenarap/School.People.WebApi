using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Security.Claims;

namespace School.People.WebApi.Middlewares
{
    public class UserValidator
    {
        private readonly RequestDelegate _next;

        public UserValidator(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value?.Trim();
            var email = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value?.Trim();

            // don't let a request through without valid user details
            if (string.IsNullOrEmpty(userId))
            {
                context.Response.StatusCode = 401;
                return;
            }

            context.Items.Add("UserId", Guid.Parse(userId));
            context.Items.Add("Email", email);

            await _next.Invoke(context);
        }
    }
}
