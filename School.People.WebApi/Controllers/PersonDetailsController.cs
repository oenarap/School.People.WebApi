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
    public class PersonDetailsController : ControllerBase
    {
        [HttpPut("{id}")]
        public Task<bool> Put([FromRoute]Guid id, [FromBody] PersonDetails details)
        {
            return commandHub.Dispatch<UpdatePersonDetailsCommand, bool>(new UpdatePersonDetailsCommand(id, details));
        }

        public PersonDetailsController(ICommandHub commandHub)
        {
            // command validators


            // command handler


            this.commandHub = commandHub;
        }

        private readonly ICommandHub commandHub;
    }
}
