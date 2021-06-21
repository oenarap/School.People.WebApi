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
    public class StudentsController : ControllerBase
    {
        [Authorize(Roles = "Registrar")]
        [HttpGet]
        public async Task<IEnumerable<IPerson>> Get()
        {
            var result = await QueryHub.Dispatch<AllStudentsQuery, AllStudentsQueryResult>(new AllStudentsQuery(this.Id)).ConfigureAwait(false);
            return result?.Data;
        }

        [HttpGet("{id}")]
        public async Task<IPerson> Get(Guid id)
        {
            var result = await QueryHub.Dispatch<StudentQuery, StudentQueryResult>(new StudentQuery(this.Id, id)).ConfigureAwait(false);
            return result?.Data;
        }

        [Authorize(Roles = "Registrar, Admin")]
        [HttpPost]
        public Task<Guid?> Post([FromBody] Person person)
        {
            return CommandHub.Dispatch<InsertStudentCommand, Guid?>(new InsertStudentCommand(this.Id, person));
        }

        [Authorize(Roles = "Registrar, Admin")]
        [HttpDelete("{id}")]
        public Task<bool> Delete(Guid id, [FromBody] Person person)
        {
            return CommandHub.Dispatch<ArchiveStudentCommand, bool>(new ArchiveStudentCommand(this.Id, person));
        }

        public StudentsController(ICommandHub commandHub, IQueryHub queryHub)
        {
            QueryHub = queryHub ?? throw new ArgumentNullException(nameof(queryHub));
            CommandHub = commandHub ?? throw new ArgumentNullException(nameof(commandHub));

            // register query handlers
            QueryHub.RegisterHandler<PeopleQueriesHandler, AllStudentsQuery, AllStudentsQueryResult>();
            QueryHub.RegisterHandler<PeopleQueriesHandler, StudentQuery, StudentQueryResult>();

            // register command handlers
            CommandHub.RegisterPreHandler<PersonValidator, InsertStudentCommand>();
            CommandHub.RegisterHandler<PeopleCommandsHandler, InsertStudentCommand, Guid?>();
            CommandHub.RegisterHandler<PeopleCommandsHandler, ArchiveStudentCommand, bool>();
        }

        private readonly ICommandHub CommandHub;
        private readonly IQueryHub QueryHub;
        private readonly Guid Id = Guid.NewGuid();
    }
}
