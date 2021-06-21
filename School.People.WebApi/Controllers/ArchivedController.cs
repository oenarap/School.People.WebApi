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
    public class ArchivedController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<IPerson>> Get()
        {
            ArchivedPeopleQueryResult result = await QueryHub.Dispatch<ArchivedPeopleQuery, ArchivedPeopleQueryResult>(new ArchivedPeopleQuery(this.Id)).ConfigureAwait(false);
            return result?.Data;
        }

        public ArchivedController(IQueryHub queryHub)
        {
            QueryHub = queryHub ?? throw new ArgumentNullException(nameof(queryHub));
            QueryHub.RegisterHandler<PeopleQueriesHandler, ArchivedPeopleQuery, ArchivedPeopleQueryResult>();
        }

        private readonly IQueryHub QueryHub;
        private readonly Guid Id = Guid.NewGuid();
    }
}
