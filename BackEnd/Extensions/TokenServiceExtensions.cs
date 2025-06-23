using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BackEnd.Extensions;

public static class TokenServiceExtensions
{
    /// <summary>
    /// Generate Token from this string.
    /// </summary>
    public static string GenerateToken(this string secretKey, int userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = System.Text.Encoding.ASCII.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Expires = DateTime.UtcNow.AddDays(1),
            Claims = new Dictionary<string, object>
            {
                { ClaimTypes.NameIdentifier, userId }
            }
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
