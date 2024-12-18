﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Services.Contract;

namespace Talabat.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration configuration;

        public AuthService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser user , UserManager<AppUser> userManager)
        {
            var authclaims = new List<Claim>() {
            new Claim(ClaimTypes.GivenName , user.UserName),
            new Claim(ClaimTypes.Email , user.Email)
            };
            var userroles = await userManager.GetRolesAsync(user);
            foreach (var role in userroles)
            {
                authclaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
            var token = new JwtSecurityToken(
                audience: configuration["JWT:ValidAudience"],
                issuer: configuration["JWT:ValidIssuer"],
                expires: DateTime.UtcNow.AddDays(double.Parse(configuration["JWT:DurationInDays"])),
               claims: authclaims,
               signingCredentials: new SigningCredentials(authkey , SecurityAlgorithms.HmacSha256Signature)
                );
          return  new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
