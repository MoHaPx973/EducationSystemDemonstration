using EducationSystem.Domain.Models;
using EducationSystem.Shared.Models;

namespace EducationSystem.App.Mappers
{
	static public class SchoolMapperExtension
	{
		static public SchoolDto? ToDto(this School? school)
		{
			if (school!=null)
			{
				return new SchoolDto
				{
					Id = school.Id,
					Name = school.Name,
					City = school.City,
				};
			}
			return null;
		}

		static public School? ToEntity(this SchoolDto? school)
		{
			if (school!=null) 
			{
				return new School
				{
					Name = school.Name,
					City = school.City,
				};
			}
			return null;
		}

	}
}