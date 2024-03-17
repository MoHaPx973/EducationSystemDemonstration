
using EducationSystem.Domain.Models;
using EducationSystem.Shared.Models;

namespace EducationSystem.App.Mappers
{
	static public class PersonMapperExtension
	{
		static public PersonDto? ToDto(this Person? item)
		{
			if (item != null)
			{
				return new PersonDto
				{
					Id = item.Id,
					Name = item.Name,
					Surname = item.Surname,
					SchoolId = item.SchoolId,
					PersonTypeId = item.PersonTypeId,
				};
			}
			return null;
		}

		static public Person? ToEntity(this PersonDto? item)
		{
			if (item != null)
			{
				return new Person
				{
					Id = item.Id,
					Name = item.Name,
					Surname = item.Surname,
					SchoolId = item.SchoolId,
					PersonTypeId = item.PersonTypeId
				};
			}
			return null;
		}
	}
}
