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
    public class CivicWorksController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IEnumerable<ICivicWork>> Get(Guid id)
        {
            var result = await QueryHub.Dispatch<CivicWorksQuery, CivicWorksQueryResult>(new CivicWorksQuery(this.Id, id)).ConfigureAwait(false);
            return result?.Data;
        }

        [HttpPost("{id}")]
        public Task<Guid?> Post(Guid id, [FromBody] CivicWork cwork)
        {
            return CommandHub.Dispatch<InsertCivicWorkCommand, Guid?>(new InsertCivicWorkCommand(this.Id, id, cwork));
        }

        [HttpPut("{id}")]
        public Task<bool> Put(Guid id, [FromBody] CivicWork cwork)
        {
            return CommandHub.Dispatch<UpdateCivicWorkCommand, bool>(new UpdateCivicWorkCommand(this.Id, cwork));
        }

        [HttpDelete("{id}")]
        public Task<bool> Delete(Guid id, [FromBody] CivicWork cwork)
        {
            return CommandHub.Dispatch<DeleteCivicWorkCommand, bool>(new DeleteCivicWorkCommand(this.Id, cwork));
        }

        public CivicWorksController(ICommandHub commandHub, IQueryHub queryHub)
        {
            QueryHub = queryHub ?? throw new ArgumentNullException(nameof(queryHub));
            CommandHub = commandHub ?? throw new ArgumentNullException(nameof(commandHub));

            // register query handlers
            QueryHub.RegisterHandler<AttributesQueriesHandler, CivicWorksQuery, CivicWorksQueryResult>();

            // register command handlers
            CommandHub.RegisterHandler<AttributesCommandsHandler, InsertCivicWorkCommand, Guid?>();
            CommandHub.RegisterHandler<AttributesCommandsHandler, UpdateCivicWorkCommand, bool>();
            CommandHub.RegisterHandler<AttributesCommandsHandler, DeleteCivicWorkCommand, bool>();
        }

        private readonly Guid Id = Guid.NewGuid();
        private readonly ICommandHub CommandHub;
        private readonly IQueryHub QueryHub;
    }
}
