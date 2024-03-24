using ETradeAPI.Application.Abstraction.Token;
using ETradeAPI.Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ETradeAPI.Application.Features.Commands.AppUserFeatures.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        public GoogleLoginCommandHandler(UserManager<AppUser> userManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { "229271353108-h9in1714gggqpe317badb1mk488p40t0.apps.googleusercontent.com" }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, settings);
            var info = new UserLoginInfo(request.Provider, payload.Subject, request.Provider);
            AppUser user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            bool result = user != null;
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                    user = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Name,
                        NameSurname = payload.Name
                        
                    };
                var id =await _userManager.CreateAsync(user);
                result = id.Succeeded;
            }

            if (result)
                await _userManager.AddLoginAsync(user, info);
            else
                throw new Exception("Invalid external auth");

            return new() { Token = _tokenHandler.CreateAccessToken(15,user) };
        }
    }
}