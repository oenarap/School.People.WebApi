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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpPut("{id}")]
        public Task<bool> Put(Guid id, [FromBody] Person value)
        {
            return CommandHub.Dispatch<UpdatePersonCommand, bool>(new UpdatePersonCommand(id, value));
        }

        public PersonController(ICommandHub commandHub)
        {
            CommandHub = commandHub ?? throw new ArgumentNullException(nameof(commandHub));
            CommandHub.RegisterHandler<PeopleCommandsHandler, UpdatePersonCommand, bool>();
        }

        private readonly ICommandHub CommandHub;
    }
}
