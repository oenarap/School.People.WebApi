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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EligibilitiesController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IEnumerable<IEligibility>> Get([FromRoute]Guid id, [FromQuery]Guid personId)
        {
            var result = await queryHub.Dispatch<EligibilitiesQuery, 
                EligibilitiesQueryResult>(new EligibilitiesQuery(id, personId)).ConfigureAwait(false);
            return result?.Data;
        }

        [HttpPost("{id}")]
        public Task<Guid?> Post([FromRoute]Guid id, [FromBody]Eligibility eligibility)
        {
            return commandHub.Dispatch<InsertEligibilityCommand, 
                Guid?>(new InsertEligibilityCommand(id, eligibility));
        }

        [HttpPut("{id}")]
        public Task<bool> Put([FromRoute] Guid id, [FromBody] Eligibility eligibility)
        {
            return commandHub.Dispatch<UpdateEligibilityCommand, bool>(new UpdateEligibilityCommand(id, eligibility));
        }

        [HttpDelete("{id}")]
        public Task<bool> Delete([FromRoute] Guid id, [FromBody] Eligibility eligibility)
        {
            return commandHub.Dispatch<DeleteEligibilityCommand, bool>(new DeleteEligibilityCommand(id, eligibility));
        }

        public EligibilitiesController(ICommandHub commandHub, IQueryHub queryHub)
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
