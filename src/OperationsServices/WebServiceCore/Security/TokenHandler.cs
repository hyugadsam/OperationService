using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelsCore.Common;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebServiceCore.Security
{
    public static class TokenHandler
    {
        public static string GenerateToken(User user, IConfiguration iConfig)
        {
            // Tu código para validar que el usuario ingresado es válido

            // Leemos el secret_key desde nuestro appseting
            var secretKey = iConfig.GetValue<string>("SecretKey");
            var JWT_Mins = iConfig.GetValue<int>("JWT_EXPIRE_MINUTES");
            var key = Encoding.ASCII.GetBytes(secretKey);
            //JWT_EXPIRE_MINUTES
            // Creamos los claims (pertenencias, características) del usuario
            var claimKeys = new[]
            {
            new Claim(ClaimTypes.Name, user.UserLogin),
            new Claim(ClaimTypes.NameIdentifier, user.Userid.ToString())
            };
            var claim = new ClaimsIdentity(claimKeys);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claim,
                // Nuestro token va a durar un día
                Expires = DateTime.UtcNow.AddMinutes(JWT_Mins),
                // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var createdToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(createdToken);
        }
    }
}
