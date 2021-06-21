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
    public class EducationsController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IEnumerable<IEducation>> Get(Guid id)
        {
            var result = await QueryHub.Dispatch<EducationsQuery, EducationsQueryResult>(new EducationsQuery(this.Id, id)).ConfigureAwait(false);
            return result?.Data;
        }

        [Authorize]
        [HttpPost("{id}")]
        public Task<Guid?> Post(Guid id, [FromBody] Education education)
        {
            return CommandHub.Dispatch<InsertEducationCommand, Guid?>(new InsertEducationCommand(this.Id, id, education));
        }

        [Authorize]
        [HttpPut("{id}")]
        public Task<bool> Put(Guid id, [FromBody] Education education)
        {
            return CommandHub.Dispatch<UpdateEducationCommand, bool>(new UpdateEducationCommand(this.Id, education));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public Task<bool> Delete(Guid id, [FromBody] Education education)
        {
            return CommandHub.Dispatch<DeleteEducationCommand, bool>(new DeleteEducationCommand(this.Id, education));
        }

        public EducationsController(ICommandHub commandHub, IQueryHub queryHub)
        {
            QueryHub = queryHub ?? throw new ArgumentNullException(nameof(queryHub));
            CommandHub = commandHub ?? throw new ArgumentNullException(nameof(commandHub));

            // register query handlers
            QueryHub.RegisterHandler<AttributesQueriesHandler, EducationsQuery, EducationsQueryResult>();

            // register command handlers
            //CommandHub.RegisterPreHandler<EducationValidator, UpdateEducationCommand>();
            //CommandHub.RegisterPreHandler<EducationValidator, InsertEducationCommand>();
            CommandHub.RegisterHandler<AttributesCommandsHandler, InsertEducationCommand, Guid?>();
            CommandHub.RegisterHandler<AttributesCommandsHandler, UpdateEducationCommand, bool>();
            CommandHub.RegisterHandler<AttributesCommandsHandler, DeleteEducationCommand, bool>();
        }
        
        private readonly Guid Id = Guid.NewGuid();
        private readonly ICommandHub CommandHub;
        private readonly IQueryHub QueryHub;
    }
}
