using EducationSystem.Provider.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace EducationSystem.Provider.AuthenticationToken
{
	public class AuthService: IAuthenticationService
	{
		private readonly AuthOptions _authOptions;

		public AuthService(AuthOptions authOptions)
		{
			_authOptions = authOptions;
		}

		public string? Authenticate(string login, string inputPassword, string storedPassword,string role)
		{
			if (!string.Equals(inputPassword, storedPassword, StringComparison.InvariantCulture))
			{
				return null;
			}

			return GetToken(login,role);
		}

		private string GetToken(string login,string role)
		{
			var identity = GetIdentity(login,role);

			var now = DateTime.UtcNow;

			var jwt = new JwtSecurityToken(
					issuer: _authOptions.Issuer,
					audience: _authOptions.Audience,
					notBefore: now,
					claims: identity.Claims,
					expires: now.Add(_authOptions.LifeTime),
					signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

			return encodedJwt;
		}

		private ClaimsIdentity GetIdentity(string nickname, string role)
		{
			var claims = new List<Claim>()
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, nickname),
				new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
			};

			ClaimsIdentity claimsIdentity = new ClaimsIdentity(
				claims, "Token",
				ClaimsIdentity.DefaultNameClaimType,
				ClaimsIdentity.DefaultRoleClaimType);

			return claimsIdentity;
		}
	}
}
