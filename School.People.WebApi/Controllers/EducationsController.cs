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
