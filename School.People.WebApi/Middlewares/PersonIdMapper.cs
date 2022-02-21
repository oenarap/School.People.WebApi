using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

namespace School.People.WebApi.Middlewares
{
    /// <summary>
    /// This middleware strips away the Id (Guid) from an outgoing 
    /// <see cref="Person"/> object, and restores it back when the same object 
    /// returns in as part of a http request.
    /// </summary>
    public class PersonIdMapper 
    {
        private readonly RequestDelegate _next;

        public PersonIdMapper(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // TODO:

            await _next.Invoke(context);
        }
    }
}
