using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Persistence.Models;

namespace WebApi.BLL;

public class JwtService(IOptions<AuthSettings> options)
{
    public string GenerateToken(Account account)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, account.Email),
            new Claim(ClaimTypes.SerialNumber, account.Id.ToString()),
            new Claim(ClaimTypes.Role, "Admin"),
            new Claim("Company", "Microsoft"),
        };
        var jwtToken = new JwtSecurityToken(
            expires: DateTime.UtcNow.Add(options.Value.Expires),
            claims: claims,
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SecretKey)),
                SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}