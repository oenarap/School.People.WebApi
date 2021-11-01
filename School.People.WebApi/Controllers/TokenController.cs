using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.People.WebApi.Services;

namespace School.People.WebApi.Controllers
{
    [Route("[controller]")]
    public class TokenController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create(string email, string password)
        {
            var result = await Service.CreateTokenAsync(email, password).ConfigureAwait(false);
            return new ObjectResult(result);
        }

        public TokenController(UserService service)
        {
            Service = service;
        }

        private readonly UserService Service;
    }
}
