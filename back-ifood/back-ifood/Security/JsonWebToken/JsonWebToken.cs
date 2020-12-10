using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ifood_back.Security
{
    public class JsonWebToken : IJsonWebToken
    {
        public TokenValidationParameters TokenValidationParameters => new TokenValidationParameters
        {
            IssuerSigningKey = JsonWebTokenSettings.SecurityKey,
            ValidateActor = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidAudience = JsonWebTokenSettings.Audience,
            ValidIssuer = JsonWebTokenSettings.Issuer
        };

        public Dictionary<string, object> Decode(string token)
        {
            return new JwtSecurityTokenHandler().ReadJwtToken(token).Payload;
        }

        public string Encode(string sub, string usuarioId)
        {
            var claims = new List<Claim>();
            claims.AddJti();
            claims.AddSub(sub);
            claims.AddUsuarioId(usuarioId);

            return new JwtSecurityTokenHandler().WriteToken(CreateJwtSecurityToken(claims));
        }

        private static JwtSecurityToken CreateJwtSecurityToken(IEnumerable<Claim> claims)
        {
            return new JwtSecurityToken
            (
                JsonWebTokenSettings.Issuer,
                JsonWebTokenSettings.Audience,
                claims,
                DateTime.UtcNow,
                JsonWebTokenSettings.Expires,
                JsonWebTokenSettings.SigningCredentials
            );
        }
    }
}
