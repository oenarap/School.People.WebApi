using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolPeople.Core.Queries;
using Apps.Communication.Abstracts;
using SchoolPeople.Core.QueryResults;
using SchoolPeople.Core.Queries.Handlers;
using SchoolPeople.Attributes.Aggregates;
using Microsoft.AspNetCore.Authorization;
using SchoolPeople.Core.Queries.Results.Handlers;

namespace School.People.WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FamilyMembersController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IFamilyMembersAggregate> Get(Guid id)
        {
            var result = await QueryHub.Dispatch<FamilyMembersAggregateQuery,
                FamilyMembersAggregateQueryResult>(new FamilyMembersAggregateQuery(this.Id, id)).ConfigureAwait(false);
            return result?.Data;
        }

        public FamilyMembersController(IQueryHub queryHub)
        {
            QueryHub = queryHub ?? throw new ArgumentNullException(nameof(queryHub));
            QueryHub.RegisterHandler<AggregateQueriesHandler, FamilyMembersAggregateQuery, FamilyMembersAggregateQueryResult>();
            QueryHub.RegisterPostHandler<FamilyMembersContributor, FamilyMembersAggregateQueryResult>();
            QueryHub.RegisterPostHandler<ChildrenContributor, FamilyMembersAggregateQueryResult>();
        }

        private readonly Guid Id = Guid.NewGuid();
        private readonly IQueryHub QueryHub;
    }
}
