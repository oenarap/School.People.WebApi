using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.People.App.Commands;
using Apps.Communication.Core;
using Microsoft.AspNetCore.Authorization;
using School.People.WebApi.Models;

namespace School.People.WebApi.Controllers
{
    [Authorize(Roles = "Registrar, Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpPut]
        public Task<bool> Put([FromRoute]Guid id, [FromBody] Person person)
        {
            return commandHub.Dispatch<UpdatePersonCommand, bool>(new UpdatePersonCommand(id, person));
        }

        public PersonController(ICommandHub commandHub)
        {
            // command validators


            // command handler


            this.commandHub = commandHub;
        }

        private readonly ICommandHub commandHub;
    }
}
