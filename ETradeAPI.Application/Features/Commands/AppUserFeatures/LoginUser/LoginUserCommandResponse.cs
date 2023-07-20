using ETradeAPI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Application.Features.Commands.AppUserFeatures.LoginUser
{
    public class LoginUserCommandResponse
    {
        public Token Token { get; set; }
        public string Message { get; set; }
    }
}
