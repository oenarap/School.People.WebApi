using System;
using System.Threading.Tasks;
using School.People.Core.Attributes;
using Microsoft.AspNetCore.Mvc;
using School.People.App.Queries;
using System.Collections.Generic;
using School.People.App.Commands;
using Apps.Communication.Core;
using Microsoft.AspNetCore.Authorization;
using School.People.App.Queries.Results;
using School.People.WebApi.Models;

namespace School.People.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorksController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IEnumerable<IWork>> Get([FromRoute] Guid id, Guid personId)
        {
            var result = await queryHub.Dispatch<WorksQuery, 
                WorksQueryResult>(new WorksQuery(id, personId)).ConfigureAwait(false);
            return result?.Data;
        }

        [Authorize]
        [HttpPost("{id}")]
        public Task<Guid?> Post([FromRoute]Guid id, [FromBody] Work work)
        {
            return commandHub.Dispatch<InsertWorkCommand, Guid?>(new InsertWorkCommand(id, work, id));
        }

        [Authorize]
        [HttpPut("{id}")]
        public Task<bool> Put([FromRoute] Guid id, [FromBody] Work work)
        {
            return commandHub.Dispatch<UpdateWorkCommand, bool>(new UpdateWorkCommand(id, work));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public Task<bool> Delete(Guid id, [FromBody] Work work)
        {
            return commandHub.Dispatch<DeleteWorkCommand, bool>(new DeleteWorkCommand(id, work));
        }

        public WorksController(ICommandHub commandHub, IQueryHub queryHub)
        {
            // query validators


            // contributors


            // command validators


            // command handler


            this.queryHub = queryHub;
            this.commandHub = commandHub;
        }

        private readonly IQueryHub queryHub;
        private readonly ICommandHub commandHub;
    }
}
