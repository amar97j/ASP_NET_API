using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Xml.Serialization;
using WebApplication2.Models;

namespace ProjectAPI.Models
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly BankContext context;
        public TokenService(IConfiguration configuration, BankContext bankcontext)
        {
            _configuration = configuration;
            this.context = bankcontext;
        }

        public (bool IsValid, string Token) GenerateToken(string username, string password)
        {

            var userAccount = context.UserAccounts.FirstOrDefault(x => x.Username == username);
            if (userAccount == null)
            {
                return (false, "");
            }
            var validPassword = BCrypt.Net.BCrypt.EnhancedVerify(password, userAccount.Password);
            if (!validPassword)
            {
                return (false, "");
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
        {
        new Claim(TokenClaimsConstant.Username, username),
        new Claim(TokenClaimsConstant.UserId, userAccount.Id.ToString()),
        new Claim(ClaimTypes.Role, userAccount.IsAdmin ? "admin" : "User")
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);
            var generatedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return (true, generatedToken);
        }
    }
}