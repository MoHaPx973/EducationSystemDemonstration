using EducationSystem.App.Mappers;
using EducationSystem.App.Storage.GenericInterface;
using EducationSystem.Domain.Models;
using EducationSystem.Domain.Relationships;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;


namespace EducationSystem.App.Interactor
{
    public class ItemInCurriculumInteractor
	{
		private IGenericRepository<ItemInCurriculum> _genericRepository;
		private IUnitWork _unitWork;
		private IGenericRepository<SchoolItem> _schoolitemGenericRepository;
		private IGenericRepository<Curriculum> _curriculumGenericRepository;
		private SchoolClassInteractor _schoolClassInteractor;

		public ItemInCurriculumInteractor
			(IGenericRepository<ItemInCurriculum> genericRepository, IUnitWork unitWork, 
			IGenericRepository<SchoolItem> schoolitemGenericRepository, IGenericRepository<Curriculum> curriculumGenericRepository,
			SchoolClassInteractor schoolClassInteractor)
		{
			_genericRepository = genericRepository;
			_unitWork = unitWork;
			_schoolitemGenericRepository = schoolitemGenericRepository;
			_curriculumGenericRepository = curriculumGenericRepository;
			_schoolClassInteractor = schoolClassInteractor;
		}

		// Методы

		// Создание
		public async Task<Response<ItemInCurriculumDto>> Insert(int itemId,int curriculumId)
		{
			if ((ItemIdIsExist(itemId))&&(CurriculumIdIsExist(curriculumId)))
			{
				ItemInCurriculum instance = new ItemInCurriculum();
				try
				{
					instance.ItemId=itemId;
					instance.CurriculumId = curriculumId;
					_genericRepository.Insert(instance);
				}
				catch(NullReferenceException ex)
				{
					return new Response<ItemInCurriculumDto>("Данные не введены", ex.Message);
				}
				catch (Exception ex)
				{
					return new Response<ItemInCurriculumDto>("Ошибка при записи в базу данных", ex.Message);
				}
					return await SaveChance(instance);
			}
				else
			{
				return new Response<ItemInCurriculumDto>("Ошибка, данные введены не верно", $"itemId = {itemId} curriculumId={curriculumId}");
			}
		}

		// Поиск по идентификатору
		public async Task<Response<ItemInCurriculumDto>> GetByIdAsync(int itemId,int curriculumId)
		{
			try
			{
				ItemInCurriculum? instance = await _genericRepository.CombinedKey(itemId, curriculumId);
				if (instance == null)
				{
					return new Response<ItemInCurriculumDto>("Связь не найдена", $"itemId = {itemId} curriculumId = {curriculumId}");
				}
				return new Response<ItemInCurriculumDto>(instance.ToDto());
			}
			catch (Exception ex)
			{
				return new Response<ItemInCurriculumDto>("Ошибка при чтении из базы данных", ex.Message);
			}
		}

		// Обновление данных - не работает
		public async Task<Response<ItemInCurriculumDto>> Update(int id, int itemId, int curriculumId)
		{
			ItemInCurriculum? instance = new ItemInCurriculum();
			try
			{
				instance = await _genericRepository.GetByIdAsync(id);
				if (instance == null) { return new Response<ItemInCurriculumDto>("Ошибка, данные введены не верно, информация об учебном плане  не найдена", $"id={id}"); }
			}
			catch (Exception ex)
			{
				return new Response<ItemInCurriculumDto>("Ошибка, данные введены не верно, учебный план не найден", ex.Message);
			}
			try
			{
				instance.ItemId = itemId;
				instance.CurriculumId = curriculumId;
				if ((ItemIdIsExist(instance.ItemId)) && (CurriculumIdIsExist(instance.CurriculumId)))
				{
					_genericRepository.Update(instance);
					return await SaveChance(instance);
				}
				return new Response<ItemInCurriculumDto>("Ошибка, данные введены не верно", $"itemId = {itemId} curriculumId={curriculumId}");
			}
			catch (Exception ex)
			{
				return new Response<ItemInCurriculumDto>("Ошибка изменения данных", ex.Message);
			}
		}

		// Удаление
		public async Task<Response<ItemInCurriculumDto>> Delete(int itemId, int curriculumId)
		{
			ItemInCurriculum? instance = new ItemInCurriculum();
			try
			{
				instance = await _genericRepository.CombinedKey(itemId, curriculumId);
				if (instance == null) { return new Response<ItemInCurriculumDto>("Ошибка, данные введены не верно, информация об свзяи не найдена", $"itemId={itemId}"); }
			}
			catch (Exception ex)
			{
				return new Response<ItemInCurriculumDto>("Ошибка, данные введены не верно, учебный план не найден", ex.Message);
			}
			try
			{
				_genericRepository.Delete(instance);
				return await SaveChance(instance);
			}
			catch (Exception ex)
			{
				return new Response<ItemInCurriculumDto>("Ошибка удаления", ex.Message);

			}
		}

        public async Task<Response<IEnumerable<ItemInCurriculumDto>>> GetByClassId(int schoolClassId)
		{
			Response<SchoolClassDto> schoolClass = new();
			Response<IEnumerable<ItemInCurriculumDto>> items = new();

            try
			{
                schoolClass=await _schoolClassInteractor.GetByIdAsync(schoolClassId);
                items.Value = GetAllIEnumerable().Value.Where(c => c.CurriculumId == schoolClass.Value.CurriculumId).ToList();
				return items;
			}
			catch(Exception ex)
			{
				return new Response<IEnumerable<ItemInCurriculumDto>>("Ошибка, класс не найден",ex.Message);
			}
		}

        // Вывод всех данных из списка
        public Response<IEnumerable<ItemInCurriculumDto>> GetAllIEnumerable()
		{
			try
			{
				return new Response<IEnumerable<ItemInCurriculumDto>>(_genericRepository.GetAllEnumerable().Select(s => s.ToDto()));
			}
			catch (Exception ex)
			{
				return new Response<IEnumerable<ItemInCurriculumDto>>("Ошибка чтения", ex.Message);
			}

		}

		
		// Вспомогательные методы

		// Сохранения в базу данных
		private async Task<Response<ItemInCurriculumDto>> SaveChance(ItemInCurriculum instance)
		{
			try
			{
				await _unitWork.Commit();
			}
			catch (Exception ex)
			{
				return new Response<ItemInCurriculumDto>("Ошибка сохранения в базу данных", ex.Message);
			}
			return new Response<ItemInCurriculumDto>(instance.ToDto());
		}
		private bool ItemIdIsExist(int Id)
		{
			try
			{
				return (_schoolitemGenericRepository.GetByIdAsync(Id).Result != null);
			}
			catch
			{
				return false;
			}
		}
		private bool CurriculumIdIsExist(int Id)
		{
			try
			{
				return (_curriculumGenericRepository.GetByIdAsync(Id).Result != null);
			}
			catch
			{
				return false;
			}
		}
	}
}
