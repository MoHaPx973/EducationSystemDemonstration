using EducationSystem.Domain.Models;
using EducationSystem.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.App.Mappers
{
	static public class AssessmentMapperExtension
	{
		static public AssessmentDto? ToDto(this Assessment? item)
		{
			if (item != null)
			{
				return new AssessmentDto
				{
					Id = item.Id,
					StudentId = item.StudentId,
					SchoolClassId = item.SchoolClassId,
					TeacherId = item.TeacherId,
					ItemId = item.ItemId,
					Date = item.Date,
					Point = item.Point
				};
			}
			return null;
		}

		static public Assessment? ToEntity(this AssessmentDto? item)
		{
			if (item != null)
			{
				return new Assessment
				{
					Id = item.Id,
					StudentId = item.StudentId,
					SchoolClassId = item.SchoolClassId,
					TeacherId = item.TeacherId,
					ItemId = item.ItemId,
					Date = item.Date,
					Point = item.Point
				};
			}
			return null;
		}
	}
}
