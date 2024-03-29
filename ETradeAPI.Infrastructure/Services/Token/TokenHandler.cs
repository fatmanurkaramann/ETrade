﻿using ETradeAPI.Application.Abstraction.Token;
using ETradeAPI.Domain.Entities.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ETradeAPI.Infrastructure.Services.Token
{
    public class TokenHandler : ITokenHandler
    {
       readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Application.DTOs.Token CreateAccessToken(int minute,AppUser user)
        {
            Application.DTOs.Token token = new Application.DTOs.Token();

            //securitykey simetriği alıyoruz
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.
                UTF8.GetBytes(_configuration["Token:Securitykey"]));

            //şifrelenmiş kimlik oluşturur
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //olouştrulacak token ayarları verilir
            token.Expiration = DateTime.UtcNow.AddMinutes(minute);
            JwtSecurityToken jwtSecurityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires:token.Expiration,
                notBefore:DateTime.UtcNow,
                signingCredentials:signingCredentials,
                claims:new List<Claim> { new(ClaimTypes.Name,user.UserName)}
                );

            //token oluşturucu sınıfından örnek alalım

            JwtSecurityTokenHandler handler = new();
            token.AccessToken= handler.WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
