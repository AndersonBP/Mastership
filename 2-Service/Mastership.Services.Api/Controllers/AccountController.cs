using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.ViewModels;
using Mastership.Services.Api.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Mastership.Services.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly JwtTokenOptions _jwtTokenOptions;
        private readonly IUserApplication _userApplication;
        public AccountController(IUserApplication userApplication)
        {
            this._userApplication = userApplication;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Login(
            [FromBody] LoginViewModel model,
            [FromServices]SignInConfigurations signInConfigurations,
            [FromServices]JwtTokenOptions jwtTokenOptions
        ) {
            var user = this._userApplication.Search(new System.Guid());

            if (user != null)
            {
                var response = GerarTokenUsuario(user);
                return Ok(new
                {
                    success = true,
                    data = response,
                    message = ""
                });
            }

            return StatusCode(401, new ResponseViewModel {
                ErrorMessage = "Fail on login."
            });
        }

        private object GerarTokenUsuario(UserViewModel user)
        {
            var jwt = new JwtSecurityToken(
                  issuer: _jwtTokenOptions.Issuer,
                  audience: _jwtTokenOptions.Audience,
                  //claims: userClaims,
                  notBefore: _jwtTokenOptions.NotBefore,
                  expires: _jwtTokenOptions.Expiration,
                  signingCredentials: _jwtTokenOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_jwtTokenOptions.ValidFor.TotalSeconds,
                user = new
                {
                    id = user.Id,
                    //claims = userClaims.Select(c => new { c.Type, c.Value })
                }
            };

            return response;
        }

    }
}