using System;
using School.People.Core.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.People.App.Commands;
using Apps.Communication.Core;
using Microsoft.AspNetCore.Authorization;

namespace School.People.WebApi.Controllers
{
    [Authorize(Roles = "Registrar, ProgCoordinator, DeptHead, EnrollingTeacher")]
    [Route("api/[controller]")]
    public class MotherController : ControllerBase
    {
        [HttpPut("{id}")]
        public Task<bool> Put([FromRoute]Guid id, [FromBody]Guid personId, [FromBody] Person mother)
        {
            return commandHub.Dispatch<UpdateMotherCommand, bool>(new UpdateMotherCommand(id, mother, personId));
        }

        public MotherController(ICommandHub commandHub)
        {
            // command validators


            // command handler


            this.commandHub = commandHub;
        }

        private readonly ICommandHub commandHub;
    }
}
