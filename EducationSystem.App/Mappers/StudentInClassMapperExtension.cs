using EducationSystem.Domain.Relationships;
using EducationSystem.Shared.Models;

namespace EducationSystem.App.Mappers
{
    static public class StudentInClassMapperExtension
	{
		static public StudentInClassDto? ToDto(this StudentInClass? item)
		{
			if (item != null)
			{
				return new StudentInClassDto
				{
					StudentId = item.StudentId,
					SchoolClassId = item.SchoolClassId
				};
			}
			return null;
		}

		static public StudentInClass? ToEntity(this StudentInClassDto? item)
		{
			if (item != null)
			{
				return new StudentInClass
				{
					StudentId = item.StudentId,
					SchoolClassId = item.SchoolClassId
				};
			}
			return null;
		}
	}
}
