using System;
using SchoolPeople.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolPeople.Core.Commands;
using Apps.Communication.Abstracts;
using Microsoft.AspNetCore.Authorization;
using SchoolPeople.Core.Commands.Handlers;

namespace School.People.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SpouseController : ControllerBase
    {
        [HttpPut("{id}")]
        public async Task<bool> Put(Guid id, [FromBody] Person value)
        {
            bool result = await CommandHub.Dispatch<UpdateSpouseCommand, bool>(new UpdateSpouseCommand(id, value)).ConfigureAwait(false);
            return result;
        }

        public SpouseController(ICommandHub commandHub)
        {
            CommandHub = commandHub ?? throw new ArgumentNullException(nameof(commandHub));
            CommandHub.RegisterHandler<PeopleCommandsHandler, UpdateSpouseCommand, bool>();
        }

        private readonly ICommandHub CommandHub;
    }
}

