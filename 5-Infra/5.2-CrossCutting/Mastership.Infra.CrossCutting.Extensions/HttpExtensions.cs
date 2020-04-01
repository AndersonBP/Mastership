using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Mastership.Infra.CrossCutting.Extensions
{
    public static class HttpExtensions
    {
        public static string GetHeader(this HttpRequest request, string key)
        {
            return request.Headers.FirstOrDefault(x => x.Key == key).Value.FirstOrDefault();
        }
    }
}
