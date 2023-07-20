using ETradeAPI.Application.Features.Commands.AppUserFeatures.CreateUser;
using ETradeAPI.Application.Features.Commands.AppUserFeatures.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETradeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
