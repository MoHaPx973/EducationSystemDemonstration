using EducationSystem.App.Interactor;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class SchoolItemController : Controller
	{
		private SchoolItemInteractor _schoolItemInteractor;
		public SchoolItemController(SchoolItemInteractor schoolItemInteractor)
		{
			this._schoolItemInteractor = schoolItemInteractor;
		}

		[HttpPost("Create")]
		public async Task<Response<SchoolItemDto>> Create(string? name)
		{
			return await _schoolItemInteractor.Insert(name);
		}

		[HttpGet("FindById")]
		public async Task<Response<SchoolItemDto>> Find(int id)
		{
			return await _schoolItemInteractor.GetByIdAsync(id);
		}
		[HttpPost("Update")]
		public async Task<Response<SchoolItemDto>> Update(int id,string? name)
		{
			return await _schoolItemInteractor.Update(id,name);
		}
		[HttpDelete("Delete")]
		public async Task<Response<SchoolItemDto>> Delete(int id)
		{
			return await _schoolItemInteractor.Delete(id);
		}
		[HttpGet("GetAllQueryable")]
		public Response<IQueryable<SchoolItemDto>> GetAllQ()
		{
			return _schoolItemInteractor.GetAllQueryable();
		}
		[HttpGet("GetPageQueryable")]
		public Response<Page<SchoolItemDto>> GetPageQueryable(int start,int count)
		{
			return _schoolItemInteractor.GetPageQueryable(start,count);
		}
		[HttpGet("GetAllByCurriculum")]
		public Response<IEnumerable<SchoolItemDto>> GetAllByCurriculum(int curriculumId)
		{
			return _schoolItemInteractor.GetAllByCurriculum(curriculumId);
		}
		[HttpGet("GetPageByCurriculum")]
		public Response<Page<SchoolItemDto>> GetPageByCurriculum(int curriculumId, int start, int count)
		{
			return _schoolItemInteractor.GetPageByCurriculum(curriculumId,start,count);
		}

	}
}
