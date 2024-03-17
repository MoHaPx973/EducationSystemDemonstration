using EducationSystem.App.Interactor;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers
{
	
	[ApiController]
	[Route("[controller]")]
	public class SchoolController : Controller
	{
		private SchoolInteractor _schoolInteractor;
		public SchoolController(SchoolInteractor schoolInteractor)
		{
			this._schoolInteractor = schoolInteractor;
		}

		//[Authorize(Roles = "admin")]
		[HttpPost("Create")]
        public async Task<Response<SchoolDto>> Create(SchoolDto school)
        {
            return await _schoolInteractor.Insert(school.Name, school.City);
        }

		//[Authorize(Roles = "admin")]
		[HttpGet("FindById")]
		public async Task<Response<SchoolDto>> Find(int id)
		{
			return await _schoolInteractor.GetByIdAsync(id);
		}

		//[Authorize(Roles = "admin")]
		[HttpPost("Update")]
		public async Task<Response<SchoolDto>> Update(int id,SchoolDto UpdateSchool)
		{
			return await _schoolInteractor.Update(id, UpdateSchool.Name, UpdateSchool.City);
		}

		//[Authorize(Roles = "admin")]
		[HttpDelete("Delete")]
		public async Task<Response<SchoolDto>> Delete(int id)
		{
			return await _schoolInteractor.Delete(id);
		}

		//[Authorize(Roles = "admin,teacher,user")]
		[HttpGet("GetAllEnumerable")]
		public Response<IEnumerable<SchoolDto>> GetAllEnumerable()
		{
			return  _schoolInteractor.GetAllEnumerable();
		}

		//[Authorize(Roles = "admin,teacher,user")]
		[HttpGet("GetPageEnumerable")]
		public Response<IEnumerable<SchoolDto>> GetPageEnumerable(int start, int count)
		{
			return _schoolInteractor.GetPageEnumerable(start, count);
		}


	}
}
