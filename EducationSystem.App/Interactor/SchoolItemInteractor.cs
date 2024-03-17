using EducationSystem.App.Mappers;
using EducationSystem.App.Storage.GenericInterface;
using EducationSystem.Domain.Models;
using EducationSystem.Domain.Relationships;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;

namespace EducationSystem.App.Interactor
{
    public class SchoolItemInteractor
	{
		private IGenericRepository<SchoolItem> _genericRepository;
		private IGenericRepository<ItemInCurriculum> _itemInCurriculumRepository;
		private IUnitWork _unitWork;
		public SchoolItemInteractor(IGenericRepository<SchoolItem> genericRepository, IUnitWork unitWork, IGenericRepository<ItemInCurriculum> itemInCurriculumRepository)
		{
			this._genericRepository = genericRepository;
			this._unitWork = unitWork;
			this._itemInCurriculumRepository = itemInCurriculumRepository;
		}

		// Методы

		// Создание
		public async Task<Response<SchoolItemDto>> Insert(string? name)
		{
			SchoolItem instance = new SchoolItem();
			try
			{
				instance.Name = name;
				_genericRepository.Insert(instance);
			}
			catch(NullReferenceException ex)
			{
				return new Response<SchoolItemDto>("Данные не введены", ex.Message);
			}
			catch (Exception ex)
			{
				return new Response<SchoolItemDto>("Ошибка при записи в базу данных", ex.Message);
			}
			return await SaveChance(instance);
		}

		// Поиск по идентификатору
		public async Task<Response<SchoolItemDto>> GetByIdAsync(int id)
		{
			try
			{
				SchoolItem? instance = await _genericRepository.GetByIdAsync(id);
				if (instance == null)
				{
					return new Response<SchoolItemDto>("Предмет не найдена", $"id = {id}");
				}
				return new Response<SchoolItemDto>(instance.ToDto());
			}
			catch (Exception ex)
			{
				return new Response<SchoolItemDto>("Ошибка при чтении из базы данных", ex.Message);
			}
		}

		// Обновление данных
		public async Task<Response<SchoolItemDto>> Update(int id, string? name)
		{
			SchoolItem? instance = new SchoolItem();
			try
			{
				instance = await _genericRepository.GetByIdAsync(id);
				if (instance == null) { return new Response<SchoolItemDto>("Ошибка, данные введены не верно, информация о предмете не найдена", $"id={id}"); }
			}
			catch (Exception ex)
			{
				return new Response<SchoolItemDto>("Ошибка, данные введены не верно, предмет не найден", ex.Message);
			}
			try
			{
				instance.Name = name;
				_genericRepository.Update(instance);
				return await SaveChance(instance);
			}
			catch(NullReferenceException ex)
			{
				return new Response<SchoolItemDto>("данные не введены", ex.Message);
			}
			catch (Exception ex)
			{
				return new Response<SchoolItemDto>("Ошибка изменения данных", ex.Message);
			}
		}

		// Удаление
		public async Task<Response<SchoolItemDto>> Delete(int id)
		{
			SchoolItem? item = new SchoolItem();
			try
			{
				item = await _genericRepository.GetByIdAsync(id);
				if (item == null) { return new Response<SchoolItemDto>("Ошибка, данные введены не верно, информация о предмете не найдена", $"id={id}"); }
			}
			catch (Exception ex)
			{
				return new Response<SchoolItemDto>("Ошибка, данные введены не верно, предмет не найдена", ex.Message);
			}
			try
			{
				_genericRepository.Delete(item);
				return await SaveChance(item);
			}
			catch (Exception ex)
			{
				return new Response<SchoolItemDto>("Ошибка удаления", ex.Message);

			}
		}

		// Вывод всех данных из списка
		public Response<IQueryable<SchoolItemDto>> GetAllQueryable()
		{
			try
			{
				return new Response<IQueryable<SchoolItemDto>>(_genericRepository.GetAllQueryable().Select(s => s.ToDto()));
			}
			catch (Exception ex)
			{
				return new Response<IQueryable<SchoolItemDto>>("Ошибка чтения", ex.Message);
			}

		}

		public Response<Page<SchoolItemDto>> GetPageQueryable(int start, int count)
		{
			try
			{
				return new Response<Page<SchoolItemDto>>(new Page<SchoolItemDto>(start, count, GetAllQueryable().Value));
			}
			catch (Exception ex)
			{
				return new Response<Page<SchoolItemDto>>("Ошибка чтения", ex.Message);
			}

		}

		public Response<IEnumerable<SchoolItemDto>> GetAllByCurriculum(int curriculumId)
		{
			try
			{
				List<ItemInCurriculum> list = new List<ItemInCurriculum>();
				list = _itemInCurriculumRepository.GetAllEnumerable().Where(r => r.CurriculumId == curriculumId).ToList();
				List<SchoolItemDto> schoolItem = new List<SchoolItemDto>();
				foreach (var item in list)
				{
					schoolItem.AddRange(_genericRepository.GetAllEnumerable().Where(i => i.Id == item.ItemId).Select(d => d.ToDto()).ToList());
				}
				return new Response<IEnumerable<SchoolItemDto>>(schoolItem.AsEnumerable());
			}
			catch (Exception ex)
			{
				return new Response<IEnumerable<SchoolItemDto>>("Ошибка чтения", ex.Message);
			}
		}

		public Response<Page<SchoolItemDto>> GetPageByCurriculum(int curriculumId,int start,int count)
		{
			try
			{
				List<ItemInCurriculum> list = new List<ItemInCurriculum>();
				list = _itemInCurriculumRepository.GetAllQueryable().Where(r => r.CurriculumId == curriculumId).ToList();
				List<SchoolItemDto> schoolItem = new List<SchoolItemDto>();
				foreach (var item in list)
				{
					schoolItem.AddRange(_genericRepository.GetAllQueryable().Where(i => i.Id == item.ItemId).Select(d => d.ToDto()).ToList());
				}
				return new Response<Page<SchoolItemDto>>(new Page<SchoolItemDto>(start,count,schoolItem.AsQueryable()));
			}
			catch (Exception ex)
			{
				return new Response<Page<SchoolItemDto>>("Ошибка чтения", ex.Message);
			}
		}

		// Вспомогательные методы

		// Сохранения в базу данных
		private async Task<Response<SchoolItemDto>> SaveChance(SchoolItem item)
		{
			try
			{
				await _unitWork.Commit();
			}
			catch (Exception ex)
			{
				return new Response<SchoolItemDto>("Ошибка сохранения в базу данных", ex.Message);
			}
			return new Response<SchoolItemDto>(item.ToDto());
		}
	}
}
