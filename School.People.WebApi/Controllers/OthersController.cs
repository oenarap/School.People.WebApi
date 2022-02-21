using System;
using School.People.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using School.People.App.Queries;
using System.Collections.Generic;
using Apps.Communication.Core;
using Microsoft.AspNetCore.Authorization;
using School.People.App.Queries.Results;
using School.People.App.Queries.Validators;
using School.People.App.Queries.Contributors;

namespace School.People.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OthersController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<IPerson>> Get([FromRoute]Guid id)
        {
            var result = await hub.Dispatch<OtherPeopleQuery, 
                OtherPeopleQueryResult>(new OtherPeopleQuery(id)).ConfigureAwait(false);
            return result?.Data.People;
        }

        public OthersController(IQueryHub hub)
        {
            // query validators
            hub.RegisterValidator<PeopleQueriesValidator, OtherPeopleQuery, OtherPeopleQueryResult>();

            // contributors
            hub.RegisterContributor<PeopleContributor, OtherPeopleQueryResult>();

            this.hub = hub;
        }

        private readonly IQueryHub hub;
    }
}
