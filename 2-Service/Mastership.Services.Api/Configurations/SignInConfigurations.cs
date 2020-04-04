using System;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Mastership.Services.Api.Configurations {
    public class SignInConfigurations {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SignInConfigurations() {
            using (var provider = new RSACryptoServiceProvider(2048)) {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
