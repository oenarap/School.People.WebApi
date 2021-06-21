using System;
using System.Text;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.People.WebApi.Data;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

namespace School.People.WebApi.Controllers
{
    [Route("[controller]")]
    public class TokenController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Create(string email, string password)
        {
            if (await GetIdentityUser(email, password) is IdentityUser user)
            {
                var userToken = $"Bearer { CreateToken(user) }";
                return new ObjectResult(userToken);
            }
            return BadRequest();
        }

        private string CreateToken(IdentityUser user)
        {
            var roles = from userRole in Context.UserRoles
                        join role in Context.Roles on userRole.RoleId equals role.Id
                        where userRole.UserId == user.Id
                        select new { userRole.UserId, userRole.RoleId, role.Name };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF32.GetBytes(Configuration.GetValue<string>("userTokey"))), 
                        SecurityAlgorithms.HmacSha256)), new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<IdentityUser> GetIdentityUser(string email, string password)
        {
            var user = await Manager.FindByEmailAsync(email);
            if (await Manager.CheckPasswordAsync(user, password)) { return user; }
            return null;
        }

        public TokenController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            Context = context;
            Manager = userManager;
            Configuration = configuration;
        }

        private readonly ApplicationDbContext Context;
        private readonly IConfiguration Configuration;
        private readonly UserManager<IdentityUser> Manager;
    }
}
