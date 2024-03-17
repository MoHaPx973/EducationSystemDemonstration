using EducationSystem.App.Interactor;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SchoolClassController : Controller
	{
		private SchoolClassInteractor _schoolClassInteractor;
		public SchoolClassController(SchoolClassInteractor schoolClassInteractor)
		{
			this._schoolClassInteractor = schoolClassInteractor;
		}

		[HttpPost("Create")]
		public async Task<Response<SchoolClassDto>> Create(int schoolId, int number, string? letter, int curriculumId, int year)
		{
			return await _schoolClassInteractor.Insert(schoolId,number,letter,curriculumId,year);
		}

		[HttpGet("FindById")]
		public async Task<Response<SchoolClassDto>> Find(int id)
		{
			return await _schoolClassInteractor.GetByIdAsync(id);
		}
		[HttpPost("Update")]
		public async Task<Response<SchoolClassDto>> Update(int id, int schoolId, int number, string? letter, int curriculumId, int year)
		{
			return await _schoolClassInteractor.Update(id,schoolId, number, letter, curriculumId, year);
		}
		[HttpDelete("Delete")]
		public async Task<Response<SchoolClassDto>> Delete(int id)
		{
			return await _schoolClassInteractor.Delete(id);
		}
		[HttpGet("GetAllQueryable")]
		public Response<IQueryable<SchoolClassDto>> GetAllQ()
		{
			return _schoolClassInteractor.GetAllQueryable();
		}
		[HttpGet("GetClassByStudentId")]
		public async Task<Response<SchoolClassDto>> GetClassByStudentId(int studentId)
		{
			return await _schoolClassInteractor.GetClassByStudentId(studentId);

        }

		[HttpGet("GetPageEnumerable")]
		public Response<IEnumerable<SchoolClassDto>> GetPageEnumerable(int start,int count)
		{
			return _schoolClassInteractor.GetPageEnumerable(start,count);
		}
		[HttpGet("GetPageEnumerableByYear")]
		public Response<IEnumerable<SchoolClassDto>> GetPageEnumerableByYear(int start, int count,int year)
		{
			return _schoolClassInteractor.GetPageEnumerableByYear(start, count,year);
		}

		[HttpGet("GetAllBySchoolId")]
		public Response<IQueryable<SchoolClassDto>> GetAllBySchoolId(int schoolId)
		{
			return _schoolClassInteractor.GetAllBySchoolId(schoolId);
		}
		[HttpGet("GetPageBySchoolId")]
		public Response<IQueryable<SchoolClassDto>> GetPageBySchoolId(int schoolId, int start, int count)
		{
			return _schoolClassInteractor.GetPageBySchoolId(schoolId,start,count);
		}

		//[HttpGet("GetAllByCurriculum")]
		//public Response<IQueryable<SchoolClassDto>> GetAllByCurriculum(int curriculumId)
		//{
		//	return _schoolClassInteractor.GetAllByCurriculum(curriculumId);
		//}
		//[HttpGet("GetPageByCurriculumId")]
		//public Response<IQueryable<SchoolClassDto>> GetPageByCurriculumId(int curriculumId, int start, int count)
		//{
		//	return _schoolClassInteractor.GetPageByCurriculumId(curriculumId, start, count);
		//}

	}
}
