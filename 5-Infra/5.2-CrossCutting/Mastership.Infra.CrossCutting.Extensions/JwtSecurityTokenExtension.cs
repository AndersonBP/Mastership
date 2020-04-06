using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace Mastership.Infra.CrossCutting.Extensions
{
  public static class JwtSecurityTokenExtension
    {
        public static ILookup<string, string> Lookup(this JwtSecurityToken jwt)
            => jwt.Claims.ToLookup(x => x.Type, x => x.Value);

        public static string GetClaimValue(this JwtSecurityToken jwt, string claim)
            => jwt.Claims
                .FirstOrDefault(x => x.Type.Equals(claim))
                .Value;
    }
}
