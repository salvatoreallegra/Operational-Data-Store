using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODSApi.Helpers
{

    public static class JwtToken
    {
        private const string SECRET_KEY = "sldkjfwivkSEGSekfjsleFWAF";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));

        public static string GenerateJwtToken()
        {
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(SIGNING_KEY, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(credentials);
            DateTime expiry = DateTime.UtcNow.AddMinutes(60);
            int ts = (int)(expiry - new DateTime(1970, 1, 1)).TotalSeconds;

            var payload = new JwtPayload
            {
                {"sub","testsubject" },
                {"name","Sal" },
                {"exp",ts },
                {"iss", "https://localhost:44317" }, //server that issues the jwt
                {"aud", "https://localhost:44317" }  //server that contains the resource
            };

            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(secToken);
            Console.WriteLine(tokenString);
            return tokenString;
        }
    }
}
