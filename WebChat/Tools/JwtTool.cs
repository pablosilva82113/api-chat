using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace WebChat.Tools
{
    public interface IJwtTool
    {
        string GenerateToken(string userId, string name, string role);
        ClaimsPrincipal? ValidateToken(string token);
    }

    public class JwtTool : IJwtTool
    {
        private readonly string _key;

        public JwtTool(IConfiguration configuration)
        {
            _key = configuration["JWT:Key"] ?? throw new ArgumentNullException("JWT:Key no configurado en appsettings.json");
        }

        /// <summary>
        /// Genera un JWT válido por 1 mes
        /// </summary>
        public string GenerateToken(string userId, string name, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMonths(1),
                signingCredentials: creds
            );

            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Valida un token JWT y devuelve los claims si es válido
        /// </summary>
        public ClaimsPrincipal? ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));

            try
            {
                var parameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero // evita tolerancia extra en expiración
                };

                var principal = tokenHandler.ValidateToken(token, parameters, out SecurityToken validatedToken);

                // Verificar algoritmo
                if (validatedToken is JwtSecurityToken jwtToken &&
                    jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    return principal;
                }
            }
            catch
            {
                // Token inválido o expirado
                return null;
            }

            return null;
        }
    }
}
