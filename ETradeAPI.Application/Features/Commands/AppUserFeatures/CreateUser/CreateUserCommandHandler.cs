using ETradeAPI.Application.Abstraction.Services;
using ETradeAPI.Application.DTOs.User;
using ETradeAPI.Application.Exceptions;
using ETradeAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETradeAPI.Application.Features.Commands.AppUserFeatures.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
         CreateUserResponse res = await _userService.CreateAsync(new()
            {
                Email = request.Email,
                NameSurname = request.NameSurname,
                Username = request.Username,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm
            });

            return new() { Message = res.Message, Succeded = res.Succeded };
        }
    }
}
