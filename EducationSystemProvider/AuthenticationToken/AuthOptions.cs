using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EducationSystem.Provider.Authentication
{
	public class AuthOptions
	{
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public TimeSpan LifeTime { get; set; }
		public string SecretKey { get; set; } 
		
		public SymmetricSecurityKey GetSymmetricSecurityKey()
		{
			return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
		}
	}
}
