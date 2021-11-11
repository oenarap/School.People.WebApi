using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using School.People.WebApi.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace School.People.WebApi.Services
{
    public class UserService
    {
        public Task<bool> RemoveUserAsync(string id)
        {
            return Task.FromResult(false);
        }

        public async Task<string> CreateTokenAsync(string email, string password)
        {
            var user = await manager.FindByEmailAsync(email).ConfigureAwait(false);

            if (user != null)
            {
                var passwordIsCorrect = await manager.CheckPasswordAsync(user, password).ConfigureAwait(false);
                
                if (passwordIsCorrect) { return CreateToken(user); }
            }
            return "Failed";
        }

        public async Task<string> AddUserAsync(string username, string email, string password)
        {
            var existingEmail = await manager.FindByEmailAsync(email).ConfigureAwait(false);
            var existingUsername = await manager.FindByNameAsync(username).ConfigureAwait(false);

            if (existingEmail == null && existingUsername == null)
            {
                var newUser = new IdentityUser(username)
                {
                    Email = email,
                    NormalizedEmail = email.Normalize(),
                    NormalizedUserName = username.Normalize()
                };

                newUser.PasswordHash = hasher.HashPassword(newUser, password);
                newUser.SecurityStamp = Guid.NewGuid().ToString();
                await context.Users.AddAsync(newUser).ConfigureAwait(false);

                var result = await context.SaveChangesAsync().ConfigureAwait(false);

                if (result > 0)
                {
                    return newUser.Id;
                }
            }
            return "Failed";
        }

        public async Task<bool> RatifyRoleAsync(string id, string role)
        {
            var normalizedRole = role.Normalize();
            var identityRole = await context.Roles.Where(r => r.NormalizedName == normalizedRole).FirstOrDefaultAsync().ConfigureAwait(false);

            if (identityRole == null)
            {
                identityRole = new IdentityRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = role,
                    NormalizedName = normalizedRole,
                    ConcurrencyStamp = Guid.NewGuid().ToString()
                };

                await context.Roles.AddAsync(identityRole).ConfigureAwait(false);
                var result = await context.SaveChangesAsync().ConfigureAwait(false);

                if (result == 0) { return false; }
            }

            if (await manager.FindByIdAsync(id) is IdentityUser user)
            {
                var userRole = new IdentityUserRole<string>() { UserId = user.Id, RoleId = identityRole.Id };

                await context.UserRoles.AddAsync(userRole).ConfigureAwait(false);
                var result = await context.SaveChangesAsync().ConfigureAwait(false);

                return result > 0;
            }
            return false;
        }

        public async Task<bool> RevokeRoleAsync(string id, string role)
        {
            var user = await manager.FindByIdAsync(id).ConfigureAwait(false);

            if (user != null)
            {
                var result = await manager.RemoveFromRoleAsync(user, role).ConfigureAwait(false);
                return result.Succeeded;
            }
            return false;
        }

        private string CreateToken(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };

            var roles = from userRole in context.UserRoles
                        join role in context.Roles on userRole.RoleId equals role.Id
                        where userRole.UserId == user.Id
                        select new { userRole.UserId, userRole.RoleId, role.Name };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var payload = new JwtPayload(claims);
            var key = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(config["Keys:TokenKey"]));
            var token = new JwtSecurityToken(new JwtHeader(new SigningCredentials(key, SecurityAlgorithms.HmacSha256)), payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserService(ApiUsersDbContext context, UserManager<IdentityUser> userManager, 
            IPasswordHasher<IdentityUser> hasher, IConfiguration configuration)
        {
            this.context = context;
            this.hasher = hasher;
            manager = userManager;
            config = configuration;
        }

        private readonly IConfiguration config;
        private readonly ApiUsersDbContext context;
        private readonly IPasswordHasher<IdentityUser> hasher;
        private readonly UserManager<IdentityUser> manager;
    }
}
