using System;
using School.People.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.People.App.Queries;
using System.Collections.Generic;
using Apps.Communication.Core;
using School.People.App.QueryResults;
using School.People.App.QueryHandlers;
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
