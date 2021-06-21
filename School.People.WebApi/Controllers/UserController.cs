using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.People.WebApi.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace School.People.WebApi.Controllers
{
    [Authorize(Roles = "Admin, Administrator")]
    [Route("[controller]")]
    public class UserController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> AddUser(string username, string email, string password)
        {
            var existingEmail = await Manager.FindByEmailAsync(email);
            var existingUsername = await Manager.FindByNameAsync(username);

            if (existingEmail == null && existingUsername == null)
            {
                var newUser = new IdentityUser(username)
                {
                    Email = email,
                    NormalizedEmail = email.Normalize(),
                    NormalizedUserName = username.Normalize()
                };
                newUser.PasswordHash = Hasher.HashPassword(newUser, password);
                newUser.SecurityStamp = Guid.NewGuid().ToString();
                await Context.Users.AddAsync(newUser);

                if (await Context.SaveChangesAsync() > 0)
                {
                    return new ObjectResult(newUser.Id);
                }
            }
            return new ObjectResult("Failed");
        }

        [HttpPut("{id}")]
        public async Task<bool> AddRole(string id, string role)
        {
            var normalizedRole = role.Normalize();
            var identityRole = await Context.Roles.Where(r => r.NormalizedName == normalizedRole).FirstOrDefaultAsync().ConfigureAwait(false);

            if (identityRole == null)
            {
                identityRole = new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = role,
                    NormalizedName = normalizedRole,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };
                await Context.Roles.AddAsync(identityRole);
                if (await Context.SaveChangesAsync() == 0) { return false; }
            }

            if (await Manager.FindByIdAsync(id) is IdentityUser user)
            {
                var userRole = new IdentityUserRole<string>() { UserId = user.Id, RoleId = identityRole.Id };

                await Context.UserRoles.AddAsync(userRole);
                return await Context.SaveChangesAsync() > 0;
            }
            return false;
        }

        [HttpDelete("{id}")]
        public async Task<bool> RemoveRole(string id, string role)
        {
            if (await Manager.FindByIdAsync(id) is IdentityUser user)
            {
                var result = await Manager.RemoveFromRoleAsync(user, role);
                return result.Succeeded;
            }
            return false;
        }

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IPasswordHasher<IdentityUser> hasher)
        {
            Context = context;
            Manager = userManager;
            Hasher = hasher;
        }

        private readonly ApplicationDbContext Context;
        private readonly IPasswordHasher<IdentityUser> Hasher;
        private readonly UserManager<IdentityUser> Manager;
    }
}
