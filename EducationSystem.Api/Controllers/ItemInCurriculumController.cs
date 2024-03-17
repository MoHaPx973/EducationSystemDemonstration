using EducationSystem.App.Interactor;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ItemInCurriculumController : Controller
	{
		private ItemInCurriculumInteractor _itemInCurriculumInteractor;
		public ItemInCurriculumController(ItemInCurriculumInteractor itemInCurriculumInteractor)
		{
			this._itemInCurriculumInteractor = itemInCurriculumInteractor;
		}

		[HttpPost("Create")]
		public async Task<Response<ItemInCurriculumDto>> Create(int itemId,int curriculumId)
		{
			return await _itemInCurriculumInteractor.Insert(itemId, curriculumId);
		}

		[HttpGet("FindById")]
		public async Task<Response<ItemInCurriculumDto>> Find(int itemId,int curriculumId)
		{
			return await _itemInCurriculumInteractor.GetByIdAsync(itemId,curriculumId);
		}
		//[HttpPost("Update")]
		//public async Task<Response<ItemInCurriculumDto>> Update(int id,int itemId, int curriculumId)
		//{
		//	return await _itemInCurriculumInteractor.Update(id, itemId, curriculumId);
		//}
		[HttpDelete("Delete")]
		public async Task<Response<ItemInCurriculumDto>> Delete(int itemId, int curriculumId)
		{
			return await _itemInCurriculumInteractor.Delete(itemId,curriculumId);
		}
		[HttpGet("GetAllIEnumerable")]
		public Response<IEnumerable<ItemInCurriculumDto>> GetAllIEnumerable()
		{
			return _itemInCurriculumInteractor.GetAllIEnumerable();
		}
		[HttpGet("GetByClassId")]
		public async Task<Response<IEnumerable<ItemInCurriculumDto>>> GetByClassId(int schoolClassId)
		{
			return await _itemInCurriculumInteractor.GetByClassId(schoolClassId);
		}
	}
}
