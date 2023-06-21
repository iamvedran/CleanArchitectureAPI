using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LeaveManagement.Application.Contracts.Identity;
using LeaveManagement.Application.Models.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LeaveManagement.Identity.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public async Task<AuthResponse> Login(AuthRequest authRequest)
    {
        if (authRequest.Email == "vedran@gmail.com" && authRequest.Password == "123456")
        {
            var token = GenerateToken(authRequest.Email);

            return new AuthResponse { Token = token };
        }

        return null;
    }

    /// <summary>
    /// Generate JWT token
    /// </summary>
    /// <param name="email"></param>
    /// <returns>JWT Token</returns>
    private string GenerateToken(string email)
    {
        var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

        var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);


        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),

        };

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}