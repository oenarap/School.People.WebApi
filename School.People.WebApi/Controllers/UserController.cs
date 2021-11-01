using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using School.People.WebApi.Services;

namespace School.People.WebApi.Controllers
{
    [Authorize(Roles = "Admin, Administrator")]
    [Route("[controller]")]
    public class UserController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> AddUser(string username, string email, string password)
{
            var result = await Service.AddUserAsync(username, email, password).ConfigureAwait(false);
            return new ObjectResult(result);
        }

        [Route("/[controller]/roles-add")]
        [HttpPut]
        public async Task<bool> AddRole(string id, string role)
        {
            var result = await Service.RatifyRoleAsync(id, role).ConfigureAwait(false);
            return result;
        }

        [Route("/[controller]/roles-revoke")]
        [HttpPut]
        public async Task<bool> RemoveRole(string id, string role)
        {
            var result = await Service.RevokeRoleAsync(id, role).ConfigureAwait(false);
            return result;
        }

        [HttpDelete]
        public async Task<bool> RemoveUser(string id)
        {
            var result = await Service.RemoveUserAsync(id).ConfigureAwait(false);
            return result;
        }

        public UserController(UserService service)
        {
            Service = service;
        }

        private readonly UserService Service;
    }
}
