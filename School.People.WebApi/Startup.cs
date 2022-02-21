using School.People.WebApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School.People.WebApi.Extensions;

namespace School.People.WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApiUsersDbContext>();
            services.AddControllers();
            services.AddApiDbContexts(config);
            services.AddJwtAuthentication(config);
            services.AddRepositories(config);
            services.AddMessagingEntities();
            services.AddServiceEntities();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseUserValidator();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }

        public Startup(IConfiguration configuration)
        {
            config = configuration;
        }

        private readonly IConfiguration config;
    }
}
