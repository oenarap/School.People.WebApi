using System.Text;
using School.People.Data;
using School.People.App.Hubs;
using School.People.Core.Repositories;
using School.People.WebApi.Data;
using Apps.Communication.Core;
using Microsoft.EntityFrameworkCore;
using School.People.Data.Repositories;
using Microsoft.IdentityModel.Tokens;
using School.People.App.QueryHandlers;
using School.People.App.Queries.Handlers;
using Microsoft.Extensions.Configuration;
using School.People.App.Commands.Handlers;
using Microsoft.Extensions.DependencyInjection;
using School.People.App.Queries.Results.Handlers;
using School.People.WebApi.Services;

namespace School.People.WebApi
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "JwtBearer";
                option.DefaultChallengeScheme = "JwtBearer";
            })
                .AddJwtBearer("JwtBearer", jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(configuration["Keys:TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = System.TimeSpan.FromMinutes(5)
                    };
                });
            return services;
        }

        public static IServiceCollection AddServiceEntities(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
            return services;
        }

        public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }

        public static IServiceCollection RegisterDbEntities(this IServiceCollection services, IConfiguration configuration)
        {
            // database context options
            DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(configuration.GetConnectionString("PeopleDbConnectionString"));
            services.AddSingleton(builder.Options);

            // database contexts
            services.AddSingleton<PeopleDbContext>();

            // hubs
            services.AddSingleton<IQueryHub, QueryHub>();
            services.AddSingleton<ICommandHub, CommandHub>();
            services.AddSingleton<IEventHub, EventHub>();

            // repositories
            services.AddSingleton<IActivePeopleRepository, ActivePeopleRepository>();
            services.AddSingleton<IStudentsRepository, StudentsRepository>();
            services.AddSingleton<IPersonnelRepository, PersonnelsRepository>();
            services.AddSingleton<IOtherPeopleRepository, OtherPeopleRepository>();
            services.AddSingleton<IArchivedPeopleRepository, ArchivedPeopleRepository>();
            services.AddSingleton<IEducationsRepository, EducationsRepository>();
            services.AddSingleton<IEligibilitiesRepository, EligibilitiesRepository>();
            services.AddSingleton<IWorksRepository, WorksRepository>();
            services.AddSingleton<ICivicWorksRepository, CivicWorksRepository>();
            services.AddSingleton<ITrainingsRepository, TrainingsRepository>();
            services.AddSingleton<IOtherInformationsRepository, OtherInformationsRepository>();
            services.AddSingleton<IFaqsRepository, FaqsRepository>();
            services.AddSingleton<IVerificationDetailsRepository, VerificationDetailsRepository>();
            services.AddSingleton<IFamilyIdsRepository, FamilyIdsRepository>();
            services.AddSingleton<IAddressIdsRepository, AddressIdsRepository>();
            services.AddSingleton<IImagesRepository, ImagesRepository>();
            services.AddSingleton<IPersonDetailsRepository, PersonDetailsRepository>();
            services.AddSingleton<IDateOfBirthsRepository, DateOfBirthsRepository>();
            services.AddSingleton<ICitizenshipsRepository, CitizenshipsRepository>();
            services.AddSingleton<IContactDetailsRepository, ContactDetailsRepository>();
            services.AddSingleton<IAgencyMemberDetailsRepository, AgencyMemberDetailsRepository>();
            services.AddSingleton<IPersonRepository, PersonRepository>();
            services.AddSingleton<IMotherIdsRepository, MotherIdsRepository>();
            services.AddSingleton<IFatherIdsRepository, FatherIdsRepository>();
            services.AddSingleton<ISpouseIdsRepository, SpouseIdsRepository>();

            // command pre-handlers
            services.AddSingleton<PersonValidator>();

            // command handlers
            services.AddSingleton<DistinctAttributesCommandsHandler>();
            services.AddSingleton<PeopleCommandsHandler>();
            services.AddSingleton<AttributesCommandsHandler>();

            // query handlers
            services.AddSingleton<AggregateQueriesHandler>();
            services.AddSingleton<PeopleQueriesHandler>();
            services.AddSingleton<AttributesQueriesHandler>();

            // query post-handlers
            services.AddSingleton<PersonDetailsContributor>();
            services.AddSingleton<DateOfBirthContributor>();
            services.AddSingleton<CitizenshipContributor>();
            services.AddSingleton<ContactDetailsContributor>();
            services.AddSingleton<AgencyMemberDetailsContributor>();

            return services;
        }
    }
}
