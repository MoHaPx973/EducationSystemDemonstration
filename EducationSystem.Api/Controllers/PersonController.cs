using EducationSystem.App.Interactor;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class PersonController : Controller
	{
		private PersonInteractor _personinteractor;
		public PersonController(PersonInteractor personinteractor)
		{
			this._personinteractor = personinteractor;
		}

		[HttpPost("Create")]
		public async Task<Response<PersonDto>> Create(PersonDto person)
		{
			return await _personinteractor.Insert(person.Name, person.Surname, person.SchoolId, person.PersonTypeId);
		}

		[HttpGet("FindById")]
		public async Task<Response<PersonDto>> Find(int id)
		{
			return await _personinteractor.GetByIdAsync(id);
		}
		[HttpPost("Update")]
		public async Task<Response<PersonDto>> Update(int id, PersonDto person)
		{
			return await _personinteractor.Update(id, person.Name, person.Surname, person.SchoolId);
		}
		[HttpDelete("Delete")]
		public async Task<Response<PersonDto>> Delete(int id)
		{
			return await _personinteractor.Delete(id);
		}
		[HttpGet("GetAllEnumerable")]
		public Response<IQueryable<PersonDto>> GetAllQ()
		{
			return _personinteractor.GetAllQueryable();
		}
		[HttpGet("GetPageEnumerable")]
		public Response<Page<PersonDto>> GetPageQueryable(int start, int count)
		{
			return _personinteractor.GetPageQueryable(start, count);
		}

		[HttpGet("GetAllByClassId")]
		public Response<IEnumerable<PersonDto>> GetAllByClassId(int classId)
		{
			return _personinteractor.GetAllByClassId(classId);
		}
		
	}
}
