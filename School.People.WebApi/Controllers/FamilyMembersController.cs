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
