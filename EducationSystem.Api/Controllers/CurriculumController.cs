using EducationSystem.App.Interactor;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CurriculumController : Controller
	{
		private CurriculumInteractor _curriculumInteractor;
		public CurriculumController(CurriculumInteractor schoolInteractor)
		{
			this._curriculumInteractor = schoolInteractor;
		}

		[HttpPost("Create")]
		public async Task<Response<CurriculumDto>> Create(int schoolId,int year)
		{
			return await _curriculumInteractor.Insert(schoolId,year);
		}

		[HttpGet("FindById")]
		public async Task<Response<CurriculumDto>> Find(int id)
		{
			return await _curriculumInteractor.GetByIdAsync(id);
		}
		[HttpPost("Update")]
		public async Task<Response<CurriculumDto>> Update(int id,int schoolId,int year)
		{
			return await _curriculumInteractor.Update(id, schoolId,year);
		}
		[HttpDelete("Delete")]
		public async Task<Response<CurriculumDto>> Delete(int id)
		{
			return await _curriculumInteractor.Delete(id);
		}
		[HttpGet("GetAllQueryable")]
		public Response<IQueryable<CurriculumDto>> GetAllQ()
		{
			return  _curriculumInteractor.GetAllQueryable();
		}
		[HttpGet("GetPageQueryable")]
		public Response<Page<CurriculumDto>> GetPageQueryable(int start,int count)
		{
			return _curriculumInteractor.GetPageQueryable(start,count);
		}

		[HttpGet("GetAllBySchoolId")]
		public Response<IQueryable<CurriculumDto>> GetAllBySchoolId(int schoolId)
		{
			return _curriculumInteractor.GetAllBySchoolId(schoolId);
		}
		[HttpGet("GetPageBySchoolId")]
		public Response<IQueryable<CurriculumDto>> GetPageBySchoolId(int schoolId, int start, int count)
		{
			return _curriculumInteractor.GetPageBySchoolId(schoolId,start,count);
		}
	}
}
