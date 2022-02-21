using System;
using School.People.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.People.App.Queries;
using Apps.Communication.Core;
using Microsoft.AspNetCore.Authorization;
using School.People.App.Queries.Results;
using System.Collections.Generic;

namespace School.People.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivedController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IEnumerable<IPerson>> Get([FromRoute] Guid id)
        {
            var result = await hub.Dispatch<ArchivedPeopleQuery, 
                ArchivedPeopleQueryResult>(new ArchivedPeopleQuery(id)).ConfigureAwait(false);
            return result?.Data.People;
        }

        public ArchivedController(IQueryHub hub)
        {
            this.hub = hub;
        }

        private readonly IQueryHub hub;
    }
}
