using EducationSystem.Domain.AuthModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.App.Storage.AuthInterface
{
	public interface IAuthRepository
	{
		Task<User?> GetByName(string login);
	}
}
