using System.Text;
using School.People.Data;
using School.People.App.Hubs;
using School.People.Core.Repositories;
using School.People.WebApi.Data;
using Apps.Communication.Core;
using Microsoft.EntityFrameworkCore;
using School.People.Data.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using School.People.WebApi.Services;
using School.People.App.Commands.Validators;
using School.People.App.Commands.Handlers;
using School.People.App.Queries.Validators;
using School.People.App.Queries.Contributors;

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

        public static IServiceCollection AddApiDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = new DbContextOptionsBuilder<PeopleDbContext>()
                .UseSqlServer(configuration.GetConnectionString("PeopleDbConnectionString"));
            
            services.AddSingleton(builder.Options);
            services.AddTransient<PeopleDbContext>();
            services.AddDbContext<ApiUsersDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            return services;
        }

        public static IServiceCollection AddMessagingEntities(this IServiceCollection services)
        {
            // hubs
            services.AddSingleton<IQueryHub, QueryHub>();
            services.AddSingleton<ICommandHub, CommandHub>();
            services.AddSingleton<IEventHub, EventHub>();

            // command validators
            services.AddTransient<PersonValidator>();
            services.AddTransient<PersonCommandsValidator>();
            services.AddTransient<AttributeCommandsValidator>();
            services.AddTransient<BirthdateValidator>();
            services.AddTransient<EducationValidator>();

            // command handlers
            services.AddTransient<DistinctAttributesCommandsHandler>();
            services.AddTransient<AttributeCommandsHandler>();
            services.AddTransient<PersonCommandsHandler>();

            // query validators 
            services.AddTransient<AggregateQueriesValidator>();
            services.AddTransient<AttributesQueriesValidator>();
            services.AddTransient<PeopleQueriesValidator>();
            services.AddTransient<PersonQueriesValidator>();

            // query contributors
            services.AddTransient<AgencyMemberDetailsContributor>();
            services.AddTransient<BirthAddressContributor>();
            services.AddTransient<BirthdateContributor>();
            services.AddTransient<CitizenshipContributor>();
            services.AddTransient<ContactDetailsContributor>();
            services.AddTransient<MotherContributor>();
            services.AddTransient<FatherContributor>();
            services.AddTransient<SpouseContributor>();
            services.AddTransient<ChildrenContributor>();
            services.AddTransient<PeopleContributor>();
            services.AddTransient<PermanentAddressContributor>();
            services.AddTransient<PersonContributor>();
            services.AddTransient<PersonDetailsContributor>();
            services.AddTransient<ResidentialAddressContributor>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IActivePeopleRepository, ActivePeopleRepository>();
            services.AddTransient<IStudentsRepository, StudentsRepository>();
            services.AddTransient<IPersonnelRepository, PersonnelsRepository>();
            services.AddTransient<IOtherPeopleRepository, OtherPeopleRepository>();
            services.AddTransient<IArchivedPeopleRepository, ArchivedPeopleRepository>();
            services.AddTransient<IEducationsRepository, EducationsRepository>();
            services.AddTransient<IEligibilitiesRepository, EligibilitiesRepository>();
            services.AddTransient<IWorksRepository, WorksRepository>();
            services.AddTransient<ICivicWorksRepository, CivicWorksRepository>();
            services.AddTransient<ITrainingsRepository, TrainingsRepository>();
            services.AddTransient<IOtherInformationsRepository, OtherInformationsRepository>();
            services.AddTransient<IFaqsRepository, FaqsRepository>();
            services.AddTransient<IVerificationDetailsRepository, VerificationDetailsRepository>();
            services.AddTransient<IFamilyIdsRepository, FamilyIdsRepository>();
            services.AddTransient<IAddressIdsRepository, AddressIdsRepository>();
            services.AddTransient<IImagesRepository, ImagesRepository>();
            services.AddTransient<IPersonDetailsRepository, PersonDetailsRepository>();
            services.AddTransient<IDateOfBirthsRepository, DateOfBirthsRepository>();
            services.AddTransient<ICitizenshipsRepository, CitizenshipsRepository>();
            services.AddTransient<IContactDetailsRepository, ContactDetailsRepository>();
            services.AddTransient<IAgencyMemberDetailsRepository, AgencyMemberDetailsRepository>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IMotherIdsRepository, MotherIdsRepository>();
            services.AddTransient<IFatherIdsRepository, FatherIdsRepository>();
            services.AddTransient<ISpouseIdsRepository, SpouseIdsRepository>();
            return services;
        }
    }
}
