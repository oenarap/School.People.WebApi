﻿using System;
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
    public class ContactDetailsController : ControllerBase
    {
        [HttpPut("{id}")]
        public Task<bool> Put(Guid id, [FromBody] ContactDetails value)
        {
            return CommandHub.Dispatch<UpdateContactDetailsCommand, bool>(new UpdateContactDetailsCommand(id, value));
        }

        public ContactDetailsController(ICommandHub commandHub)
        {
            CommandHub = commandHub ?? throw new ArgumentNullException(nameof(commandHub));
            CommandHub.RegisterHandler<DistinctAttributesCommandsHandler, UpdateContactDetailsCommand, bool>();
        }

        private readonly ICommandHub CommandHub;
    }
}