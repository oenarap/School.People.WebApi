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
    public class AgencyMemberDetailsController : ControllerBase
    {
        [HttpPut("{id}")]
        public Task<bool> Put(Guid id, [FromBody] AgencyMemberDetails value)
        {
            return CommandHub.Dispatch<UpdateAgencyMemberDetailsCommand, bool>(new UpdateAgencyMemberDetailsCommand(id, value));
        }

        public AgencyMemberDetailsController(ICommandHub commandHub)
        {
            CommandHub = commandHub ?? throw new ArgumentNullException(nameof(commandHub));
            CommandHub.RegisterHandler<DistinctAttributesCommandsHandler, UpdateAgencyMemberDetailsCommand, bool>();
        }

        private readonly ICommandHub CommandHub;
    }
}
