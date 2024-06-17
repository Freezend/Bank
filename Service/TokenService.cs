using Bank.Interfaces;
using Bank.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Bank.Service {
	public class TokenService : ITokenService {
		private readonly IConfiguration _configuration;
		private readonly SymmetricSecurityKey _symmetricSecurityKey;
		public TokenService(IConfiguration configuration) {
			_configuration = configuration;
			_symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"] ?? "r78g6fd87f6g8df76b8cv6b87df687b6d8"));
		}
		public string CreateToken(AppUser appUser) {
			var claims = new List<Claim> {
				new Claim(JwtRegisteredClaimNames.Email, appUser.Email ?? ""),
				new Claim(JwtRegisteredClaimNames.GivenName, appUser.UserName ?? ""),
			};

			var creds = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);

			var tokenDescriptor = new SecurityTokenDescriptor {
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(7),
				SigningCredentials = creds,
				Issuer = _configuration["JWT:Issuer"],
				Audience = _configuration["JWT:Audience"]
			};

			var tokenHandler = new JwtSecurityTokenHandler();

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}
	}
}
