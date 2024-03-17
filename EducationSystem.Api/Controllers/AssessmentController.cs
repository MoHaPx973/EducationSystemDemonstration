using EducationSystem.App.Interactor;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EducationSystem.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class AssessmentController : Controller
	{
		private AssessmentInteractor _assessmentInteractor;
		public AssessmentController(AssessmentInteractor assessmentInteractor)
		{
			this._assessmentInteractor = assessmentInteractor;
		}
	
		[HttpGet("Create")]
		public async Task<Response<AssessmentDto>> Insert(int studentId, int teacherId, int schoolClassId, int itemId, DateTime date, int point)
		{
			return await _assessmentInteractor.Insert(studentId, teacherId, schoolClassId, itemId, date, point);
		}

		[HttpGet("GetAllByStudentIdItemId")]
		public Response<IEnumerable<AssessmentDto>> GetAllByStudentIdItemId(int studentId, int itemId)
		{
			return _assessmentInteractor.GetAllByStudentIdItemId(studentId,itemId);
		}

		[HttpPost("Delete")]
		public async Task<Response<AssessmentDto>> Delete(int id)
		{
			return await _assessmentInteractor.Delete(id);
		}


		//[HttpGet("FindById")]
		//public async Task<Response<CurriculumDto>> Find(int id)
		//{
		//	return await _curriculumInteractor.GetByIdAsync(id);
		//}
		//[HttpPost("Update")]
		//public async Task<Response<CurriculumDto>> Update(int id,int schoolId,int year)
		//{
		//	return await _curriculumInteractor.Update(id, schoolId,year);
		//}

		//[HttpGet("GetAllQueryable")]
		//public Response<IQueryable<CurriculumDto>> GetAllQ()
		//{
		//	return  _curriculumInteractor.GetAllQueryable();
		//}
		//[HttpGet("GetPageQueryable")]
		//public Response<Page<CurriculumDto>> GetPageQueryable(int start,int count)
		//{
		//	return _curriculumInteractor.GetPageQueryable(start,count);
		//}

		//[HttpGet("GetAllBySchoolId")]
		//public Response<IQueryable<CurriculumDto>> GetAllBySchoolId(int schoolId)
		//{
		//	return _curriculumInteractor.GetAllBySchoolId(schoolId);
		//}
		//[HttpGet("GetPageBySchoolId")]
		//public Response<IQueryable<CurriculumDto>> GetPageBySchoolId(int schoolId, int start, int count)
		//{
		//	return _curriculumInteractor.GetPageBySchoolId(schoolId,start,count);
		//}
	}
}
