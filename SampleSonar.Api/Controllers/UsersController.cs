using MediatR;
using Microsoft.AspNetCore.Mvc;
using SampleSonar.Api.Extensions;
using SampleSonar.Core.Commands;
using SampleSonar.Core.Queries;
using SampleSonar.Data.Entities;
using SampleSonar.Data.Models.Requests;
using SampleSonar.Data.Models.Responses;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleSonar.Api.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiExplorerSettings(GroupName = "v1")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET: api/<UsersController>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
        public async Task<IActionResult> Get()
        {
            var result = await mediator.Send(new GetUsersQuery()).ConfigureAwait(false);
            return Ok(result);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<User>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(int id)
        {
            var result = await mediator.Send(new GetUserByIdQuery(id)).ConfigureAwait(false);
            if(result != null)
                return Ok(result);
            return NoContent();
        }

        // POST api/<UsersController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<User>))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(Response<User>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<User>))]
        public async Task<IActionResult> Post([FromBody] CreateUserRequest value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());
            
            Response<User> response = await mediator.Send(new CreateUserCommand(value)).ConfigureAwait(false);

            if(response.Code == "63")
                return Conflict(response);

            return Ok(response);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<User>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<User>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<User>))]
        public async Task<IActionResult> Put(int id, [FromBody] CreateUserRequest value)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            Response<User> response = await mediator.Send(new UpdateUserCommand(id, value)).ConfigureAwait(false);

            if (response.Code == "25")
                return NotFound(response);

            return Ok(response);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<User>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<User>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<User>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Response<User>))]
        public async Task<IActionResult> Delete([Required]int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessage());

            Response<User> response = await mediator.Send(new DeleteUserCommand(id)).ConfigureAwait(false);

            if (response.Code == "25")
                return NotFound(response);

            return Ok(response);
        }
    }
}
