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
    public class MotherController : ControllerBase
    {
        [HttpPut("{id}")]
        public Task<bool> Put(Guid id, [FromBody] Person value)
        {
            return CommandHub.Dispatch<UpdateMotherCommand, bool>(new UpdateMotherCommand(id, value));
        }

        public MotherController(ICommandHub commandHub)
        {
            CommandHub = commandHub ?? throw new ArgumentNullException(nameof(commandHub));
            CommandHub.RegisterHandler<PeopleCommandsHandler, UpdateMotherCommand, bool>();
        }

        private readonly ICommandHub CommandHub;
    }
}
