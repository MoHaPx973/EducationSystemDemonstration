using EducationSystem.Domain.Models;
using EducationSystem.Shared.Models;


namespace EducationSystem.App.Mappers
{
	static public class SchoolClassMapperExtension
	{
		static public SchoolClassDto? ToDto(this SchoolClass? item)
		{
			if (item != null)
			{
				return new SchoolClassDto
				{
					Id = item.Id,
					SchoolId = item.SchoolId,
					Number = item.Number,
					Letter = item.Letter,
					CurriculumId = item.CurriculumId,
					YearFormation = item.YearFormation
				};
			}
			return null;
		}

		static public SchoolClass? ToEntity(this SchoolClassDto? item)
		{
			if (item != null)
			{
				return new SchoolClass
				{
					Id = item.Id,
					SchoolId = item.SchoolId,
					Number = item.Number,
					Letter = item.Letter,
					CurriculumId = item.CurriculumId,
					YearFormation = item.YearFormation
				};
			}
			return null;
		}
	}
}
