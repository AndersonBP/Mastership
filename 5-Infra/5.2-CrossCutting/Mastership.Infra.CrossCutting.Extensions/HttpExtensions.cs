using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Mastership.Infra.CrossCutting.Extensions
{
    public static class HttpExtensions
    {
        public static string GetHeader(this HttpRequest request, string key)
        {
            return request.Headers.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
        }

        public static JwtSecurityToken GetToken(this HttpRequest request)
        {
            var token = request.Headers["Authorization"].First().Replace("Bearer ", "");
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadToken(token) as JwtSecurityToken;
        }
    }
}
