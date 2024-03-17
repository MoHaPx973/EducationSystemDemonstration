using EducationSystem.App.Interactor;
using EducationSystem.Domain.Models;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class StudentInClassController : Controller
	{
		private StudentInClassInteractor _studentInClassInteractor;
		public StudentInClassController(StudentInClassInteractor studentInClassInteractor)
		{
			this._studentInClassInteractor = studentInClassInteractor;
		}

		[HttpPost("Create")]
		public async Task<Response<StudentInClassDto>> Create(int personId,int classId)
		{
			return await _studentInClassInteractor.Insert(personId, classId);
		}

		[HttpGet("FindById")]
		public async Task<Response<StudentInClassDto>> Find(int studentId,int schoolId)
		{
			return await _studentInClassInteractor.GetByIdAsync(studentId,schoolId);
		}
		//[HttpPost("Update")]
		//public async Task<Response<ItemInCurriculumDto>> Update(int id,int itemId, int curriculumId)
		//{
		//	return await _studentInClassInteractor.Update(id, itemId, curriculumId);
		//}
		[HttpDelete("Delete")]
		public async Task<Response<StudentInClassDto>> Delete(int personId, int classId)
		{
			return await _studentInClassInteractor.Delete(personId,classId);
		}
		[HttpGet("GetAllEnumerable")]
		public Response<IEnumerable<StudentInClassDto>> GetAllEnumerable()
		{
			return _studentInClassInteractor.GetAllEnumerable();
		}
	}
}
