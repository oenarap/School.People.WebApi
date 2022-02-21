using System;
using System.Threading.Tasks;
using School.People.Core.Attributes;
using Microsoft.AspNetCore.Mvc;
using School.People.App.Queries;
using System.Collections.Generic;
using School.People.App.Commands;
using Apps.Communication.Core;
using School.People.App.Commands.Handlers;
using School.People.App.Commands.Validators;
using School.People.App.Queries.Contributors;
using School.People.App.Queries.Results;
using School.People.App.Queries.Validators;
using School.People.WebApi.Models;

namespace School.People.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CivicWorksController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IEnumerable<ICivicWork>> Get(Guid id)
        {
            var result = await queryHub.Dispatch<CivicWorksQuery, CivicWorksQueryResult>(new CivicWorksQuery(id, id)).ConfigureAwait(false);
            return result?.Data;
        }

        [HttpPost("{id}")]
        public Task<Guid?> Post(Guid id, [FromBody] CivicWork cwork)
        {
            return commandHub.Dispatch<InsertCivicWorkCommand, Guid?>(new InsertCivicWorkCommand(id, cwork, id));
        }

        [HttpPut("{id}")]
        public Task<bool> Put(Guid id, [FromBody] CivicWork cwork)
        {
            return commandHub.Dispatch<UpdateCivicWorkCommand, bool>(new UpdateCivicWorkCommand(id, cwork));
        }

        [HttpDelete("{id}")]
        public Task<bool> Delete([FromRoute]Guid id, [FromBody] CivicWork cwork)
        {
            return commandHub.Dispatch<DeleteCivicWorkCommand, bool>(new DeleteCivicWorkCommand(id, cwork));
        }

        public CivicWorksController(ICommandHub commandHub, IQueryHub queryHub)
        {
            // query validators
            queryHub.RegisterValidator<AttributesQueriesValidator, CivicWorksQuery, CivicWorksQueryResult>();

            // contributors
            queryHub.RegisterContributor<PeopleContributor, AllPersonnelQueryResult>();
            queryHub.RegisterContributor<PersonContributor, PersonnelQueryResult>();

            // command validators
            commandHub.RegisterValidator<InsertCivicWorkCommand, AttributeCommandsValidator>();
            commandHub.RegisterValidator<UpdateCivicWorkCommand, AttributeCommandsValidator>();
            commandHub.RegisterValidator<DeleteCivicWorkCommand, AttributeCommandsValidator>();

            // command handler
            commandHub.RegisterHandler<InsertCivicWorkCommand, AttributeCommandsHandler, Guid?>();
            commandHub.RegisterHandler<UpdateCivicWorkCommand, AttributeCommandsHandler, bool>();
            commandHub.RegisterHandler<DeleteCivicWorkCommand, AttributeCommandsHandler, bool>();

            this.queryHub = queryHub;
            this.commandHub = commandHub;
        }

        private readonly ICommandHub commandHub;
        private readonly IQueryHub queryHub;
    }
}
