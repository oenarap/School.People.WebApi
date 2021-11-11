using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.People.App.Queries;
using Apps.Communication.Core;
using School.People.Core.Attributes;
using Microsoft.AspNetCore.Authorization;
using School.People.Core.DTOs.Aggregates;
using School.People.App.Queries.Results;
using School.People.App.Queries.Models;
using School.People.App.Queries.Validators;
using School.People.App.Queries.Contributors;

namespace School.People.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyMembersController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<FamilyMembersQueryData> Get([FromRoute]Guid id, [FromQuery]Guid personId)
        {
            var result = await hub.Dispatch<FamilyMembersQuery,
                FamilyMembersQueryResult>(new FamilyMembersQuery(id, personId)).ConfigureAwait(false);
            return result?.Data;
        }

        public FamilyMembersController(IQueryHub hub)
        {
            // validator
            hub.RegisterValidator<AggregateQueriesValidator, FamilyMembersQuery, FamilyMembersQueryResult>();

            // contributors
            hub.RegisterContributor<FamilyMembersContributor, FamilyMembersQueryResult>();
            hub.RegisterContributor<ChildrenContributor, FamilyMembersQueryResult>();

            this.hub = hub;
        }

        private readonly IQueryHub hub;
    }
}
