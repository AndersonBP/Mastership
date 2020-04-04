using AutoMapper;
using Mastership.Domain.DTO;
using Mastership.Domain.Interfaces.Application;
using Mastership.Domain.ViewModels;
using Mastership.Services.Api.Configurations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace Mastership.Services.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    [Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserApplication _userApplication;
        private readonly ISubsidiaryApplication _subsidiaryApplication;

        public AccountController(
            IMapper mapper,
            IUserApplication userApplication,
            ISubsidiaryApplication subsidiaryApplication
        ) {
            this._mapper = mapper;
            this._userApplication = userApplication;
            this._subsidiaryApplication = subsidiaryApplication;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Login(
            [FromBody] LoginViewModel model,
            [FromHeader(Name = "DomainName")][Required] string domainName,
            [FromServices]SignInConfigurations signInConfigurations,
            [FromServices]JwtTokenOptions jwtTokenOptions
        ) {
            bool credentialsChecked = false;

            var user = this._userApplication.Authenticate(domainName, model);
            if (user != null) {
                credentialsChecked = true;
            }

            if (credentialsChecked) {
                var subisidiary = this._subsidiaryApplication.GetSubsidiaryByUser(this._mapper.Map<UserDTO>(user));
                return Ok(this.GetJwtResponse(user, subisidiary, signInConfigurations, jwtTokenOptions));
            } else {
                return Unauthorized();
            }
        }

        private object GetJwtResponse(UserViewModel user, SubsidiaryDTO subsidiaryDTO, SignInConfigurations signingConfigurations, JwtTokenOptions tokenConfigurations) {
            ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(user.Id.ToString("N"), "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString("N")),
                        new Claim("user_id", user.Id.ToString("N")),
                        new Claim("role", user.UserType.ToString()),
                        new Claim("subsidiary", subsidiaryDTO.Id.ToString("N"))
                    }
                );

            DateTime creationDate = DateTime.Now;
            DateTime expirationDate = creationDate +
                TimeSpan.FromSeconds(tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = creationDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);

            return new {
                authenticated = true,
                created = creationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }

    }
}