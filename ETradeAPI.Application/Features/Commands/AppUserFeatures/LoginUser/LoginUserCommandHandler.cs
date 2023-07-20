using ETradeAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Application.Features.Commands.AppUserFeatures.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _signInManager;

        public LoginUserCommandHandler(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
          AppUser user=  await _userManager.FindByNameAsync(request.UsernameOrEmail);

            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
            }
          SignInResult result =  await _signInManager.CheckPasswordSignInAsync(user,request.Password,false);

            if (result.Succeeded)
            {
                //yetkileri belirle
            }

            return new();

        }
    }
}
