using ETradeAPI.Application.Abstraction.Services;
using ETradeAPI.Application.DTOs.User;
using ETradeAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Persistance.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<Domain.Entities.Identity.AppUser> _manager;

        public UserService(UserManager<AppUser> manager)
        {
            _manager = manager;
        }

        public async Task<CreateUserResponse> CreateAsync(CreateUser model)
        {
            IdentityResult result = await _manager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Email = model.Email,
                NameSurname = model.NameSurname
            },model.Password);

            CreateUserResponse res = new() { Succeded= result.Succeeded};
            if (result.Succeeded)
                res.Message = "Kullanıcı oluşturuldu";
            else
                res.Message = "Hata";

            return res;

        }
    }
}
