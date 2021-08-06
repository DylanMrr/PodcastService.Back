using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using PodcastService.Identity.Api.Data;
using PodcastService.Identity.Api.Data.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PodcastService.Identity.Api.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace PodcastService.Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthService _authService;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IAuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegisterDto userRegisterDto)
        {
            if (ModelState.IsValid)
            {
                //todo автомаппер
                User user = new User()
                {
                    Email = userRegisterDto.Email,
                    Login = userRegisterDto.Login,
                    UserName = userRegisterDto.Login
                };
                var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.Errors);
                }
                await _userManager.AddToRoleAsync(user, "user");
                return StatusCode(StatusCodes.Status201Created);
            }
            return StatusCode(StatusCodes.Status400BadRequest, "TODO");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login (UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, userLoginDto.Password))
            {
                var cred = _authService.GetSigningCredentials();
                var claims = await _authService.GetClaimsAsync(user);
                var tokenOptions = _authService.GenerateTokenOptions(cred, claims);

                var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                return Ok(token);
            }
            return Unauthorized("Invalid credentials");
        }

        
    }
}