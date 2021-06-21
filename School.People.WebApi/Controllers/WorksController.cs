using System;
using SchoolPeople.DTOs;
using System.Threading.Tasks;
using SchoolPeople.Attributes;
using Microsoft.AspNetCore.Mvc;
using SchoolPeople.Core.Queries;
using System.Collections.Generic;
using SchoolPeople.Core.Commands;
using Apps.Communication.Abstracts;
using SchoolPeople.Core.QueryResults;
using SchoolPeople.Core.QueryHandlers;
using Microsoft.AspNetCore.Authorization;
using SchoolPeople.Core.Commands.Handlers;

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
