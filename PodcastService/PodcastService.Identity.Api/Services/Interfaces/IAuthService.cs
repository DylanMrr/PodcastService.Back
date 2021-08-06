using Microsoft.IdentityModel.Tokens;
using PodcastService.Identity.Api.Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PodcastService.Identity.Api.Services.Interfaces
{
    public interface IAuthService
    {
        SigningCredentials GetSigningCredentials();
        JwtSecurityToken GenerateTokenOptions(SigningCredentials credentials, List<Claim> claims);
        Task<List<Claim>> GetClaimsAsync(User user);
    }
}
