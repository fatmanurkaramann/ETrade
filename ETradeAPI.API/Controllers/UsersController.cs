﻿using ETradeAPI.Application.Features.Commands.AppUserFeatures.CreateUser;
using ETradeAPI.Application.Features.Commands.AppUserFeatures.GoogleLogin;
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
           CreateUserCommandResponse res = await _mediator.Send(request);
            return Ok(res);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest request)
        {
           LoginUserCommandResponse res = await _mediator.Send(request);
            return Ok(res);
        }
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest request)
        {
            GoogleLoginCommandResponse res = await _mediator.Send(request);
            return Ok(res);
        }
    }
}
