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
    public class CitizenshipController : ControllerBase
    {
        [HttpPut("{id}")]
        public Task<bool> Put(Guid id, [FromBody] Citizenship value)
        {
            return CommandHub.Dispatch<UpdateCitizenshipCommand, bool>(new UpdateCitizenshipCommand(id, value));
        }

        public CitizenshipController(ICommandHub commandHub)
        {
            CommandHub = commandHub ?? throw new ArgumentNullException(nameof(commandHub));
            CommandHub.RegisterHandler<DistinctAttributesCommandsHandler, UpdateCitizenshipCommand, bool>();
        }

        private readonly ICommandHub CommandHub;
    }
}
