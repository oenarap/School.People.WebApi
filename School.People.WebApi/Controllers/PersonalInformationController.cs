using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Apps.Communication.Core;
using Microsoft.AspNetCore.Authorization;
using School.People.App.Queries;
using School.People.App.Queries.Results;
using School.People.App.Queries.Models;
using School.People.App.Queries.Contributors;
using School.People.App.Queries.Validators;
using School.People.Core.DTOs.Aggregates;

namespace School.People.WebApi.Controllers
{
    [Authorize(Roles = "Registrar, ProgCoordinator, DeptHead, EnrollingTeacher")]
    [Route("api/[controller]")]
    public class PersonalInformationController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<PersonalInformationQueryData> Get([FromRoute]Guid id, [FromQuery]Guid personId)
        {
            var result = await hub.Dispatch<PersonalInformationQuery,
                PersonalInformationQueryResult>(new PersonalInformationQuery(id, personId)).ConfigureAwait(false);
            return result?.Data;
        }

        public PersonalInformationController(IQueryHub hub)
        {
            this.hub = hub;

            // validator
            hub.RegisterValidator<AggregateQueriesValidator, PersonalInformationQuery, PersonalInformationQueryResult>();
            
            // contributors
            hub.RegisterContributor<BirthdateContributor, PersonalInformationQueryResult>();
            hub.RegisterContributor<PersonDetailsContributor, PersonalInformationQueryResult>();
            hub.RegisterContributor<AgencyMemberDetailsContributor, PersonalInformationQueryResult>();
            hub.RegisterContributor<CitizenshipContributor, PersonalInformationQueryResult>();
            hub.RegisterContributor<ContactDetailsContributor, PersonalInformationQueryResult>();
        }

        private readonly IQueryHub hub;
    }
}
