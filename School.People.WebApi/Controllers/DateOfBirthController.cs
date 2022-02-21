using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.People.App.Commands;
using Apps.Communication.Core;
using Microsoft.AspNetCore.Authorization;
using School.People.App.Commands.Handlers;
using School.People.WebApi.Models;

namespace School.People.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DateOfBirthController : ControllerBase
    {
        [HttpPut("{id}")]
        public Task<bool> Put(Guid id, [FromBody] DateOfBirth value)
        {
            return commandHub.Dispatch<UpdateDateOfBirthCommand, bool>(new UpdateDateOfBirthCommand(id, value));
        }

        public DateOfBirthController(ICommandHub commandHub)
        {
            // command validators


            // command handler


            this.commandHub = commandHub;
        }

        private readonly ICommandHub commandHub;
    }
}
