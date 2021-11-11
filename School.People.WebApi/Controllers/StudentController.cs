using System;
using School.People.Core;
using School.People.Core.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.People.App.Queries;
using School.People.App.Commands;
using System.Collections.Generic;
using Apps.Communication.Core;
using Microsoft.AspNetCore.Authorization;
using School.People.App.Commands.Handlers;
using School.People.App.Queries.Results;
using School.People.App.Queries.Contributors;
using School.People.App.Queries.Validators;
using School.People.App.Commands.Validators;

namespace School.People.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [Route("/api/[controller]/all")]
        [Authorize(Roles = "Registrar, Admin")]
        [HttpGet]
        public async Task<IEnumerable<IPerson>> GetAll([FromRoute]Guid id)
        {
            var result = await queryHub.Dispatch<AllStudentsQuery, 
                AllStudentsQueryResult>(new AllStudentsQuery(id)).ConfigureAwait(false);
            return result?.Data.ToPersonArray();
        }

        [HttpGet("{id}")]
        public async Task<IPerson> GetStudent([FromRoute]Guid id, [FromQuery]Guid studentId)
        {
            var result = await queryHub.Dispatch<StudentQuery, 
                StudentQueryResult>(new StudentQuery(id, studentId)).ConfigureAwait(false);
            return result?.Data;
        }

        [Authorize(Roles = "Registrar, Admin")]
        [HttpPost]
        public Task<Guid?> InsertStudent([FromRoute]Guid id, [FromBody] Person person)
        {
            return commandHub.Dispatch<InsertStudentCommand, Guid?>(new InsertStudentCommand(id, person));
        }

        [Authorize(Roles = "Registrar, Admin")]
        [HttpDelete]
        public Task<bool> DeleteStudent([FromRoute] Guid id, [FromBody] Person person)
        {
            return commandHub.Dispatch<ArchiveStudentCommand, bool>(new ArchiveStudentCommand(id, person));
        }

        public StudentController(ICommandHub commandHub, IQueryHub queryHub)
        {
            // query validators
            queryHub.RegisterValidator<PeopleQueriesValidator, AllStudentsQuery, AllStudentsQueryResult>();
            queryHub.RegisterValidator<PersonQueriesValidator, StudentQuery, StudentQueryResult>();

            // contributors
            queryHub.RegisterContributor<PeopleContributor, AllStudentsQueryResult>();
            queryHub.RegisterContributor<PersonContributor, StudentQueryResult>();

            // command validators
            commandHub.RegisterValidator<InsertStudentCommand, PersonValidator>();

            // command handler
            commandHub.RegisterHandler<InsertStudentCommand, PersonCommandsHandler, Guid?>();
            commandHub.RegisterHandler<ArchiveStudentCommand, PersonCommandsHandler, bool>();

            this.queryHub = queryHub;
            this.commandHub = commandHub;
        }

        private readonly ICommandHub commandHub;
        private readonly IQueryHub queryHub;
    }
}
