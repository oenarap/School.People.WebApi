using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using School.People.Data;

namespace School.People.WebApi.Data
{
    public class SchoolPeopleDbContextFactory : IDesignTimeDbContextFactory<SchoolPeopleDbContext>
    {
        public SchoolPeopleDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PeopleDbContext>();
            builder.UseSqlServer(@"Data Source=SURFACE-RMD10\SQLEXPRESS;Initial Catalog=SchoolPeopleDb;Integrated Security=True");

            return new SchoolPeopleDbContext(builder.Options);
        }
    }

    public class ApiUsersDbContextFactory : IDesignTimeDbContextFactory<ApiUsersDbContext>
    {
        public ApiUsersDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApiUsersDbContext>();
            builder.UseSqlServer(@"Data Source=SURFACE-RMD10\\SQLEXPRESS;Initial Catalog=School.People.UsersDb;Integrated Security=True");

            return new ApiUsersDbContext(builder.Options);
        }
    }

    public class SchoolPeopleDbContext : PeopleDbContext
    {
        public SchoolPeopleDbContext(DbContextOptions<PeopleDbContext> options)
            : base(options) { }
    }

    public class ApiUsersDbContext : IdentityDbContext
    {
        public ApiUsersDbContext(DbContextOptions<ApiUsersDbContext> options)
            : base(options) { }
    }
}
