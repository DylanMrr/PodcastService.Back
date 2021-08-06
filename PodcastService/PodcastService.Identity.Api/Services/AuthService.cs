using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PodcastService.Identity.Api.Data;
using PodcastService.Identity.Api.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PodcastService.Identity.Api.Services
{
    public class AuthService: IAuthService
    {
        private readonly IConfigurationSection _jwtSettings;
        private readonly UserManager<User> _userManager;
        public AuthService(
            IConfiguration configuration,
            UserManager<User> userManager)
        {
            _jwtSettings = configuration.GetSection("JwtSettings");
            _userManager = userManager;
        }

        public SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_jwtSettings.GetSection("SecurityKey").Value);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public JwtSecurityToken GenerateTokenOptions(SigningCredentials credentials, List<Claim> claims)
        {
            return new JwtSecurityToken(
                issuer: _jwtSettings.GetSection("ValidIssuer").Value,
                audience: _jwtSettings.GetSection("ValidAudience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(
                        _jwtSettings.GetSection("ExpiresInMinutes").Value)),
                signingCredentials: credentials);
        }

        public async Task<List<Claim>> GetClaimsAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }
    }
}
