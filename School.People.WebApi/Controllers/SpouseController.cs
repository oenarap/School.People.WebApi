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
    public class SpouseController : ControllerBase
    {
        [HttpPut("{id}")]
        public async Task<bool> Put([FromRoute]Guid id, Guid personId, [FromBody] Person value)
        {
            bool result = await commandHub.Dispatch<UpdateSpouseCommand, 
                bool>(new UpdateSpouseCommand(id, value, personId)).ConfigureAwait(false);
            return result;
        }

        public SpouseController(ICommandHub commandHub)
        {
            // command validators


            // command handler


            this.commandHub = commandHub;
        }

        private readonly ICommandHub commandHub;
    }
}

