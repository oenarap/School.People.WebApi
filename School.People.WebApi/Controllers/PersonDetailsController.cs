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
    public class PersonDetailsController : ControllerBase
    {
        [HttpPut("{id}")]
        public Task<bool> Put(Guid id, [FromBody] PersonDetails value)
        {
            return CommandHub.Dispatch<UpdatePersonDetailsCommand, bool>(new UpdatePersonDetailsCommand(id, value));
        }

        public PersonDetailsController(ICommandHub commandHub)
        {
            CommandHub = commandHub ?? throw new ArgumentNullException(nameof(commandHub));
            CommandHub.RegisterHandler<DistinctAttributesCommandsHandler, UpdatePersonDetailsCommand, bool>();
        }

        private readonly ICommandHub CommandHub;
    }
}
