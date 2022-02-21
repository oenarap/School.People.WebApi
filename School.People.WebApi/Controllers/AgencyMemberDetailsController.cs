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
    public class AgencyMemberDetailsController : ControllerBase
    {
        [HttpPut("{id}")]
        public Task<bool> Put([FromRoute]Guid id, [FromBody] AgencyMemberDetails details)
        {
            return commandHub.Dispatch<UpdateAgencyMemberDetailsCommand, 
                bool>(new UpdateAgencyMemberDetailsCommand(id, details));
        }

        public AgencyMemberDetailsController(ICommandHub commandHub)
        {
            // command validators


            // command handler


            this.commandHub = commandHub;
        }

        private readonly ICommandHub commandHub;
    }
}
