using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Application.Features.Commands.AppUserFeatures.CreateUser
{
    public class CreateUserCommandResponse
    {
        public bool Succeded { get; set; }=true;
        public string Message { get; set; } = "Kullanıcı kaydı başarıyla oluştu";
    }
}
