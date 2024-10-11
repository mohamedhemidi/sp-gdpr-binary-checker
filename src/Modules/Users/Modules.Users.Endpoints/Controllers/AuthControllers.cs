
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Modules.Users.Application.Users.DTOs;
using Modules.Users.Application.Users.Commands.Register;
using Modules.Users.Application.Users.Queries.Login;
namespace Modules.Users.Endpoints.Controllers
{
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {

        private readonly ISender _mediator;
        public AuthController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO request)
        {


            var command = new RegisterCommand(request);
            var registerResult = await _mediator.Send(command);

            return Ok(registerResult);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var query = new LoginQuery(request);
            var loginResult = await _mediator.Send(query);

            return Ok(loginResult);
        }
    }
}
