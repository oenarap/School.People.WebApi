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
