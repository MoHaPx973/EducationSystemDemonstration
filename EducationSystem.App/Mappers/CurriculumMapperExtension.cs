using EducationSystem.Domain.Models;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;

namespace EducationSystem.App.Mappers
{
	static public class CurriculumMapperExtension
	{
		static public CurriculumDto? ToDto(this Curriculum? item)
		{
			if (item!=null)
			{
				return new CurriculumDto
				{
					Id = item.Id,
					SchoolId = item.SchoolId,
					YearFormation = item.YearFormation
				};
			}
			return null;
		}

		static public Curriculum? ToEntity(this CurriculumDto? item)
		{
			if (item != null)
			{
				return new Curriculum
				{
					Id = item.Id,
					SchoolId = item.SchoolId,
					YearFormation = item.YearFormation
				};
			}
			return null;
		}
	}
}