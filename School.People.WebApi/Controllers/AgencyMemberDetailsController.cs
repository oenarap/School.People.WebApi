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
