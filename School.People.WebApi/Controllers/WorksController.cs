using System;
using School.People.Core.DTOs;
using System.Threading.Tasks;
using School.People.Core.Attributes;
using Microsoft.AspNetCore.Mvc;
using School.People.App.Queries;
using System.Collections.Generic;
using School.People.App.Commands;
using Apps.Communication.Core;
using School.People.App.QueryResults;
using School.People.App.QueryHandlers;
using Microsoft.AspNetCore.Authorization;
using School.People.App.Commands.Handlers;

namespace School.People.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorksController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IEnumerable<IWork>> Get(Guid id)
        {
            var result = await QueryHub.Dispatch<WorksQuery, WorksQueryResult>(new WorksQuery(this.Id, id)).ConfigureAwait(false);
            return result?.Data;
        }

        [Authorize]
        [HttpPost("{id}")]
        public Task<Guid?> Post(Guid id, [FromBody] Work work)
        {
            return CommandHub.Dispatch<InsertWorkCommand, Guid?>(new InsertWorkCommand(this.Id, id, work));
        }

        [Authorize]
        [HttpPut("{id}")]
        public Task<bool> Put(Guid id, [FromBody] Work work)
        {
            return CommandHub.Dispatch<UpdateWorkCommand, bool>(new UpdateWorkCommand(this.Id, work));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public Task<bool> Delete(Guid id, [FromBody] Work work)
        {
            return CommandHub.Dispatch<DeleteWorkCommand, bool>(new DeleteWorkCommand(this.Id, work));
        }

        public WorksController(ICommandHub commandHub, IQueryHub queryHub)
        {
            QueryHub = queryHub ?? throw new ArgumentNullException(nameof(queryHub));
            CommandHub = commandHub ?? throw new ArgumentNullException(nameof(commandHub));

            // register query handlers
            QueryHub.RegisterHandler<AttributesQueriesHandler, WorksQuery, WorksQueryResult>();

            // register command handlers
            CommandHub.RegisterHandler<AttributesCommandsHandler, InsertWorkCommand, Guid?>();
            CommandHub.RegisterHandler<AttributesCommandsHandler, UpdateWorkCommand, bool>();
            CommandHub.RegisterHandler<AttributesCommandsHandler, DeleteWorkCommand, bool>();
        }

        private readonly Guid Id = Guid.NewGuid();
        private readonly ICommandHub CommandHub;
        private readonly IQueryHub QueryHub;
    }
}
