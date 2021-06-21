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
    public class FatherController : ControllerBase
    {
        [HttpPut("{id}")]
        public Task<bool> Put(Guid id, [FromBody] Person value)
        {
            return CommandHub.Dispatch<UpdateFatherCommand, bool>(new UpdateFatherCommand(id, value));
        }

        public FatherController(ICommandHub commandHub)
        {
            CommandHub = commandHub ?? throw new ArgumentNullException(nameof(commandHub));
            CommandHub.RegisterHandler<PeopleCommandsHandler, UpdateFatherCommand, bool>();
        }

        private readonly ICommandHub CommandHub;
    }
}