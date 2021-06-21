using System;
using SchoolPeople;
using SchoolPeople.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolPeople.Core.Queries;
using SchoolPeople.Core.Commands;
using System.Collections.Generic;
using Apps.Communication.Abstracts;
using SchoolPeople.Core.QueryResults;
using SchoolPeople.Core.QueryHandlers;
using Microsoft.AspNetCore.Authorization;
using SchoolPeople.Core.Commands.Handlers;

namespace School.People.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnelController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<IPerson>> Get()
        {
            AllPersonnelQueryResult result = await QueryHub.Dispatch<AllPersonnelQuery, AllPersonnelQueryResult>(new AllPersonnelQuery(this.Id)).ConfigureAwait(false);
            return result?.Data;
        }

        [HttpGet("{id}")]
        public async Task<IPerson> Get(Guid id)
        {
            PersonnelQueryResult result = await QueryHub.Dispatch<PersonnelQuery, PersonnelQueryResult>(new PersonnelQuery(this.Id, id)).ConfigureAwait(false);
            return result?.Data;
        }

        [Authorize(Roles = "Admin, HRMO")]
        [HttpPost]
        public Task<Guid?> Post([FromBody] Person person)
        {
            return CommandHub.Dispatch<InsertPersonnelCommand, Guid?>(new InsertPersonnelCommand(this.Id, person));
        }

        [Authorize(Roles = "Admin, HRMO")]
        [HttpDelete("{id}")]
        public Task<bool> Delete(Guid id, [FromBody] Person person)
        {
            return CommandHub.Dispatch<ArchivePersonnelCommand, bool>(new ArchivePersonnelCommand(this.Id, person));
        }

        public PersonnelController(ICommandHub commandHub, IQueryHub queryHub)
        {
            QueryHub = queryHub ?? throw new ArgumentNullException(nameof(queryHub));
            CommandHub = commandHub ?? throw new ArgumentNullException(nameof(commandHub));

            // register query handlers
            QueryHub.RegisterHandler<PeopleQueriesHandler, AllPersonnelQuery, AllPersonnelQueryResult>();
            QueryHub.RegisterHandler<PeopleQueriesHandler, PersonnelQuery, PersonnelQueryResult>();

            // register command handlers
            CommandHub.RegisterPreHandler<PersonValidator, InsertPersonnelCommand>();
            CommandHub.RegisterHandler<PeopleCommandsHandler, InsertPersonnelCommand, Guid?>();
            CommandHub.RegisterHandler<PeopleCommandsHandler, ArchivePersonnelCommand, bool>();
        }

        private readonly IQueryHub QueryHub;
        private readonly ICommandHub CommandHub;
        private readonly Guid Id = Guid.NewGuid();
    }
}
