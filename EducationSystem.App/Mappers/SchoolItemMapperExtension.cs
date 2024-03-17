using EducationSystem.Domain.Models;
using EducationSystem.Shared.Models;

namespace EducationSystem.App.Mappers
{
	static public class SchoolItemMapperExtension
	{
		static public SchoolItemDto? ToDto(this SchoolItem? item)
		{
			if (item!=null)
			{
				return new SchoolItemDto
				{
					Id = item.Id,
					Name = item.Name
				};
			}
			return null;
		}

		static public SchoolItem? ToEntity(this SchoolItemDto? item)
		{
			if (item != null)
			{
				return new SchoolItem
				{
					Id = item.Id,
					Name = item.Name
				};
			}
			return null;
		}
	}
}
