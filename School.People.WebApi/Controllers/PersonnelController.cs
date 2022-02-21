using System;
using School.People.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.People.App.Queries;
using School.People.App.Commands;
using System.Collections.Generic;
using Apps.Communication.Core;
using Microsoft.AspNetCore.Authorization;
using School.People.App.Commands.Handlers;
using School.People.App.Queries.Contributors;
using School.People.App.Queries.Results;
using School.People.App.Queries.Validators;
using School.People.App.Commands.Validators;
using School.People.WebApi.Models;

namespace School.People.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<IPerson>> Get([FromRoute]Guid id)
        {
           var result = await queryHub.Dispatch<AllPersonnelQuery, 
               AllPersonnelQueryResult>(new AllPersonnelQuery(id)).ConfigureAwait(false);
            return result?.Data.People;
        }

        [HttpGet("{id}")]
        public async Task<IPerson> Get([FromRoute]Guid id, [FromQuery]Guid personnelId)
        {
            var result = await queryHub.Dispatch<PersonnelQuery, 
                PersonnelQueryResult>(new PersonnelQuery(id, personnelId)).ConfigureAwait(false);
            return result?.Data.Person;
        }

        [Authorize(Roles = "Admin, HRMO")]
        [HttpPost]
        public Task<Guid?> Post([FromRoute]Guid id, [FromBody] Person person)
        {
            return commandHub.Dispatch<InsertPersonnelCommand, Guid?>(new InsertPersonnelCommand(id, person));
        }

        [Authorize(Roles = "Admin, HRMO")]
        [HttpDelete("{id}")]
        public Task<bool> Delete([FromRoute]Guid id, [FromBody] Person person)
        {
            return commandHub.Dispatch<ArchivePersonnelCommand, bool>(new ArchivePersonnelCommand(id, person));
        }

        public PersonnelController(ICommandHub commandHub, IQueryHub queryHub)
        {
            // query validators
            queryHub.RegisterValidator<PeopleQueriesValidator, AllPersonnelQuery, AllPersonnelQueryResult>();
            queryHub.RegisterValidator<PersonQueriesValidator, PersonnelQuery, PersonnelQueryResult>();

            // contributors
            queryHub.RegisterContributor<PeopleContributor, AllPersonnelQueryResult>();
            queryHub.RegisterContributor<PersonContributor, PersonnelQueryResult>();

            // command validators
            commandHub.RegisterValidator<InsertPersonnelCommand, PersonValidator>();

            // command handler
            commandHub.RegisterHandler<InsertPersonnelCommand, PersonCommandsHandler, Guid?>();
            commandHub.RegisterHandler<ArchivePersonnelCommand, PersonCommandsHandler, bool>();

            this.queryHub = queryHub;
            this.commandHub = commandHub;
        }

        private readonly IQueryHub queryHub;
        private readonly ICommandHub commandHub;
    }
}
