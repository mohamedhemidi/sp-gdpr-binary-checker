using Microsoft.AspNetCore.Mvc;
using Entries.DTOs;

namespace Entries.Controllers
{
    [Route("api/entries")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("check")]
        public async Task<IActionResult> CheckString([FromBody] BinaryCheckRequestDTO request)
        {
            /*var command = new RegisterCommand(request);*/
            /*var registerResult = await _mediator.Send(command);*/
            /*return Ok(registerResult);*/
            return Ok(request);
        }
    }
}
