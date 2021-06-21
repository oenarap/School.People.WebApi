using System;
using School.People.Core.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.People.App.Commands;
using Apps.Communication.Core;
using Microsoft.AspNetCore.Authorization;
using School.People.App.Commands.Handlers;

namespace School.People.WebApi.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DateOfBirthController : ControllerBase
    {
        [HttpPut("{id}")]
        public Task<bool> Put(Guid id, [FromBody] DateOfBirth value)
        {
            return CommandHub.Dispatch<UpdateDateOfBirthCommand, bool>(new UpdateDateOfBirthCommand(id, value));
        }

        public DateOfBirthController(ICommandHub commandHub)
        {
            CommandHub = commandHub ?? throw new ArgumentNullException(nameof(commandHub));
            CommandHub.RegisterHandler<DistinctAttributesCommandsHandler, UpdateDateOfBirthCommand, bool>();
        }

        private readonly ICommandHub CommandHub;
    }
}
