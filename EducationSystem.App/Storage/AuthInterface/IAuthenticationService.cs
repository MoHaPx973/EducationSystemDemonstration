using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.Provider.AuthenticationToken
{
	public interface IAuthenticationService
	{
		string? Authenticate(string login, string inputPassword, string storedPassword,string role);
	}
}
