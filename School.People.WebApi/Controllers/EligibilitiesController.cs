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
    public class EligibilitiesController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IEnumerable<IEligibility>> Get(Guid id)
        {
            var result = await QueryHub.Dispatch<EligibilitiesQuery, EligibilitiesQueryResult>(new EligibilitiesQuery(this.Id, id)).ConfigureAwait(false);
            return result?.Data;
        }

        [HttpPost("{id}")]
        public Task<Guid?> Post(Guid id, [FromBody] Eligibility eligibility)
        {
            return CommandHub.Dispatch<InsertEligibilityCommand, Guid?>(new InsertEligibilityCommand(this.Id, id, eligibility));
        }

        [HttpPut("{id}")]
        public Task<bool> Put(Guid id, [FromBody] Eligibility eligibility)
        {
            return CommandHub.Dispatch<UpdateEligibilityCommand, bool>(new UpdateEligibilityCommand(this.Id, eligibility));
        }

        [HttpDelete("{id}")]
        public Task<bool> Delete(Guid id, [FromBody] Eligibility eligibility)
        {
            return CommandHub.Dispatch<DeleteEligibilityCommand, bool>(new DeleteEligibilityCommand(this.Id, eligibility));
        }

        public EligibilitiesController(ICommandHub commandHub, IQueryHub queryHub)
        {
            QueryHub = queryHub ?? throw new ArgumentNullException(nameof(queryHub));
            CommandHub = commandHub ?? throw new ArgumentNullException(nameof(commandHub));

            // register query handlers
            QueryHub.RegisterHandler<AttributesQueriesHandler, EligibilitiesQuery, EligibilitiesQueryResult>();

            // register command handlers
            CommandHub.RegisterHandler<AttributesCommandsHandler, InsertEligibilityCommand, Guid?>();
            CommandHub.RegisterHandler<AttributesCommandsHandler, UpdateEligibilityCommand, bool>();
            CommandHub.RegisterHandler<AttributesCommandsHandler, DeleteEligibilityCommand, bool>();
        }

        private readonly Guid Id = Guid.NewGuid();
        private readonly ICommandHub CommandHub;
        private readonly IQueryHub QueryHub;
    }
}
