
using EducationSystem.Domain.AuthModels;
using EducationSystem.Domain.Models;
using EducationSystem.Shared.Models;

namespace EducationSystem.App.Mappers
{
	static public class UserMapperExtension
	{
		static public UserDto? ToDto(this User? item)
		{
			if (item != null)
			{
				return new UserDto
				{
					Id = item.Id,
					Login = item.Login,
					Password = item.Password,
					PersonId = item.PersonId,
					Role = item.Role
				};
			}
			return null;
		}

		static public User? ToEntity(this UserDto? item)
		{
			if (item != null)
			{
				return new User
				{
					Id = item.Id,
					Login = item.Login,
					Password = item.Password,
					PersonId = item.PersonId,
					Role = item.Role
				};
			}
			return null;
		}
	}
}
