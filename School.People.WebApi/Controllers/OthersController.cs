using System;
using SchoolPeople;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolPeople.Core.Queries;
using System.Collections.Generic;
using Apps.Communication.Abstracts;
using SchoolPeople.Core.QueryResults;
using SchoolPeople.Core.QueryHandlers;
using Microsoft.AspNetCore.Authorization;

namespace School.People.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OthersController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<IPerson>> Get()
        {
            OtherPeopleQueryResult result = await QueryHub.Dispatch<OtherPeopleQuery, OtherPeopleQueryResult>(new OtherPeopleQuery(this.Id)).ConfigureAwait(false);
            return result?.Data;
        }

        public OthersController(IQueryHub queryHub)
        {
            QueryHub = queryHub ?? throw new ArgumentNullException(nameof(queryHub));
            QueryHub.RegisterHandler<PeopleQueriesHandler, OtherPeopleQuery, OtherPeopleQueryResult>();
        }

        private readonly Guid Id = Guid.NewGuid();
        private readonly IQueryHub QueryHub;
    }
}
