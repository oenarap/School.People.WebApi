using System;
using School.People.Core.DTOs;
using System.Threading.Tasks;
using School.People.Core.Attributes;
using Microsoft.AspNetCore.Mvc;
using School.People.App.Queries;
using System.Collections.Generic;
using School.People.App.Commands;
using Apps.Communication.Core;
using Microsoft.AspNetCore.Authorization;
using School.People.App.Commands.Handlers;
using School.People.App.Commands.Validators;
using School.People.App.Queries.Contributors;
using School.People.App.Queries.Results;
using School.People.App.Queries.Validators;

namespace School.People.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IEnumerable<IEducation>> Get([FromRoute]Guid id, [FromQuery]Guid personId)
        {
            var result = await queryHub.Dispatch<EducationsQuery, 
                EducationsQueryResult>(new EducationsQuery(id, personId)).ConfigureAwait(false);
            return result?.Data;
        }

        [Authorize]
        [HttpPost("{id}")]
        public Task<Guid?> Post([FromRoute]Guid id, [FromBody] Education education)
        {
            return commandHub.Dispatch<InsertEducationCommand, Guid?>(new InsertEducationCommand(id, education, id));
        }

        [Authorize]
        [HttpPut("{id}")]
        public Task<bool> Put(Guid id, [FromBody] Education education)
        {
            return commandHub.Dispatch<UpdateEducationCommand, bool>(new UpdateEducationCommand(id, education));
        }

        [Authorize]
        [HttpDelete("{id}")]
        public Task<bool> Delete(Guid id, [FromBody] Education education)
        {
            return commandHub.Dispatch<DeleteEducationCommand, bool>(new DeleteEducationCommand(id, education));
        }

        public EducationsController(ICommandHub commandHub, IQueryHub queryHub)
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
