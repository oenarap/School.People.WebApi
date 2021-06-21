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
