using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Application.Features.Commands.AppUserFeatures.GoogleLogin
{
    public class GoogleLoginCommandRequest:IRequest<GoogleLoginCommandResponse>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Id { get; set; }
        public string IdToken { get; set; }
        public string Name { get; set; }
        public string Provider { get; set; }




    }
}
