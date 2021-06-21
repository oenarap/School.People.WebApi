using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.People.App.Queries;
using Apps.Communication.Core;
using School.People.App.QueryResults;
using School.People.App.Queries.Handlers;
using School.People.Core.Attributes.Aggregates;
using Microsoft.AspNetCore.Authorization;
using School.People.App.Queries.Results.Handlers;

namespace School.People.WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonalInformationController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IPersonalInformationAggregate> Get(Guid id)
        {
            var result = await QueryHub.Dispatch<PersonalInformationAggregateQuery, 
                PersonalInformationAggregateQueryResult>(new PersonalInformationAggregateQuery(this.Id, id)).ConfigureAwait(false);
            return result?.Data;
        }

        public PersonalInformationController(IQueryHub queryHub)
        {
            QueryHub = queryHub ?? throw new ArgumentNullException(nameof(queryHub));
            QueryHub.RegisterHandler<AggregateQueriesHandler, PersonalInformationAggregateQuery, PersonalInformationAggregateQueryResult>();
            QueryHub.RegisterPostHandler<PersonDetailsContributor, PersonalInformationAggregateQueryResult>();
            QueryHub.RegisterPostHandler<DateOfBirthContributor, PersonalInformationAggregateQueryResult>();
            QueryHub.RegisterPostHandler<CitizenshipContributor, PersonalInformationAggregateQueryResult>();
            QueryHub.RegisterPostHandler<ContactDetailsContributor, PersonalInformationAggregateQueryResult>();
            QueryHub.RegisterPostHandler<AgencyMemberDetailsContributor, PersonalInformationAggregateQueryResult>();
        }

        private readonly Guid Id = Guid.NewGuid();
        private readonly IQueryHub QueryHub;
    }
}
