using EducationSystem.Domain.Relationships;
using EducationSystem.Shared.Models;

namespace EducationSystem.App.Mappers
{
    static public class ItemInCurriculumMapperExtension
	{
		static public ItemInCurriculumDto? ToDto(this ItemInCurriculum? item)
		{
			if (item != null)
			{
				return new ItemInCurriculumDto
				{
					CurriculumId = item.CurriculumId,
					ItemId = item.ItemId
				};
			}
			return null;
		}

		static public ItemInCurriculum? ToEntity(this ItemInCurriculumDto? item)
		{
			if (item != null)
			{
				return new ItemInCurriculum
				{
					CurriculumId = item.CurriculumId,
					ItemId = item.ItemId
				};
			}
			return null;
		}
	}
}
