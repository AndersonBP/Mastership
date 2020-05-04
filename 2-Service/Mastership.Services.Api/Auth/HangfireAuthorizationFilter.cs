using Hangfire.Dashboard;
using Mastership.Services.Api.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Mastership.Services.Api.Auth
{
    //public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    //{
    //public bool Authorize(DashboardContext context)
    //    {
    //        var httpContext = context.GetHttpContext();
    //        return httpContext.User.Identity.IsAuthenticated;
    //    }
    //}
    public class HangfireDashboardJwtAuthorizationFilter : IDashboardAuthorizationFilter
    {
        private static readonly string HangFireCookieName = "HangFireCookie";
        private static readonly int CookieExpirationMinutes = 60;
        private TokenValidationParameters tokenValidationParameters = new TokenValidationParameters();
        private string role;

        public HangfireDashboardJwtAuthorizationFilter(SignInConfigurations signInConfigurations, JwtTokenOptions jwtTokenOptions, string role = null)
        {
            this.tokenValidationParameters.IssuerSigningKey = signInConfigurations.Key;
            this.tokenValidationParameters.ValidAudience = jwtTokenOptions.Audience;
            this.tokenValidationParameters.ValidIssuer = jwtTokenOptions.Issuer;
            this.tokenValidationParameters.ValidateIssuerSigningKey = true;
            this.tokenValidationParameters.ValidateLifetime = true;
            this.tokenValidationParameters.ClockSkew = TimeSpan.Zero;
            this.role = role;
        }

        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            var access_token = String.Empty;
            var setCookie = false;

            // try to get token from query string
            if (httpContext.Request.Query.ContainsKey("access_token"))
            {
                access_token = httpContext.Request.Query["access_token"].FirstOrDefault();
                setCookie = true;
            }
            else
            {
                access_token = httpContext.Request.Cookies[HangFireCookieName];
            }

            if (String.IsNullOrEmpty(access_token))
            {
                return false;
            }

            try
            {
                SecurityToken validatedToken = null;
                JwtSecurityTokenHandler hand = new JwtSecurityTokenHandler();
                var claims = hand.ValidateToken(access_token, this.tokenValidationParameters, out validatedToken);
                //return claims.Identity.IsAuthenticated;
                //TODO: APPLYROLE
                //if (!String.IsNullOrEmpty(this.role) && !claims.IsInRole(this.role))
                //{
                //    return false;
                //}
                if (!claims.Identity.IsAuthenticated)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            if (setCookie)
            {
                httpContext.Response.Cookies.Append(HangFireCookieName,
                access_token,
                new CookieOptions()
                {
                    Expires = DateTime.Now.AddHours(CookieExpirationMinutes)
                });
            }


            return true;
        }
    }
}
