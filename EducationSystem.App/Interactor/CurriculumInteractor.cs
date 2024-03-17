using EducationSystem.App.Mappers;
using EducationSystem.App.Storage.GenericInterface;
using EducationSystem.Domain.Models;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;
using System.ComponentModel.Design;
using System.Xml.Linq;


namespace EducationSystem.App.Interactor
{
	public class CurriculumInteractor
	{
		private IGenericRepository<Curriculum> _genericRepository;
		private IUnitWork _unitWork;
		private IGenericRepository<School> _schoolgenericRepository;
		public CurriculumInteractor(IGenericRepository<Curriculum> genericRepository, IUnitWork unitWork, IGenericRepository<School> schoolgenericRepository)
		{
			this._genericRepository = genericRepository;
			this._unitWork = unitWork;
			this._schoolgenericRepository = schoolgenericRepository;
		}

		// Методы

		// Создание
		public async Task<Response<CurriculumDto>> Insert(int schoolId,int yearformation)
		{
			if (SchoolIdIsExist(schoolId))
			{
				Curriculum instance = new Curriculum();
				try
				{
					instance.YearFormation=yearformation;
					instance.SchoolId = schoolId;
					_genericRepository.Insert(instance);
				}
				catch(NullReferenceException ex)
				{
					return new Response<CurriculumDto>("Данные не введены", ex.Message);
				}
				catch (Exception ex)
				{
					return new Response<CurriculumDto>("Ошибка при записи в базу данных", ex.Message);
				}
					return await SaveChance(instance);
			}
				else
			{
				return new Response<CurriculumDto>("Ошибка, данные введены не верно, данной школы не существует", $"schoolId = {schoolId}");
			}
		}

		// Поиск по идентификатору
		public async Task<Response<CurriculumDto>> GetByIdAsync(int id)
		{
			try
			{
				Curriculum? instance = await _genericRepository.GetByIdAsync(id);
				if (instance == null)
				{
					return new Response<CurriculumDto>("Учебный план не найден", $"id = {id}");
				}
				return new Response<CurriculumDto>(instance.ToDto());
			}
			catch (Exception ex)
			{
				return new Response<CurriculumDto>("Ошибка при чтении из базы данных", ex.Message);
			}
		}

		// Обновление данных
		public async Task<Response<CurriculumDto>> Update(int id, int schoolId, int yearformation)
		{
			Curriculum? instance = new Curriculum();
			try
			{
				instance = await _genericRepository.GetByIdAsync(id);
				if (instance == null) { return new Response<CurriculumDto>("Ошибка, данные введены не верно, информация об учебном плане  не найдена", $"id={id}"); }
			}
			catch (Exception ex)
			{
				return new Response<CurriculumDto>("Ошибка, данные введены не верно, учебный план не найден", ex.Message);
			}
			try
			{
				instance.SchoolId = schoolId;
				instance.YearFormation = yearformation;
				if (SchoolIdIsExist(instance.SchoolId))
				{
					_genericRepository.Update(instance);
					return await SaveChance(instance);
				}
				else return new Response<CurriculumDto>("Ошибка, данные введены не верно, данной школы не существует", $"schoolId = {schoolId}");
			}
			catch (Exception ex)
			{
				return new Response<CurriculumDto>("Ошибка изменения данных", ex.Message);
			}
		}

		// Удаление
		public async Task<Response<CurriculumDto>> Delete(int id)
		{
			Curriculum? instance = new Curriculum();
			try
			{
				instance = await _genericRepository.GetByIdAsync(id);
				if (instance == null) { return new Response<CurriculumDto>("Ошибка, данные введены не верно, информация об учебном плане не найдена", $"id={id}"); }
			}
			catch (Exception ex)
			{
				return new Response<CurriculumDto>("Ошибка, данные введены не верно, учебный план не найден", ex.Message);
			}
			try
			{
				_genericRepository.Delete(instance);
				return await SaveChance(instance);
			}
			catch (Exception ex)
			{
				return new Response<CurriculumDto>("Ошибка удаления", ex.Message);

			}
		}

		// Вывод всех данных из списка
		public Response<IQueryable<CurriculumDto>> GetAllQueryable()
		{
			try
			{
				return new Response<IQueryable<CurriculumDto>>(_genericRepository.GetAllQueryable().Select(s => s.ToDto()));
			}
			catch (Exception ex)
			{
				return new Response<IQueryable<CurriculumDto>>("Ошибка чтения", ex.Message);
			}

		}

		public Response<Page<CurriculumDto>> GetPageQueryable(int start, int count)
		{
			try
			{
				return new Response<Page<CurriculumDto>>(new Page<CurriculumDto>(start, count, GetAllQueryable().Value));
			}
			catch (Exception ex)
			{
				return new Response<Page<CurriculumDto>>("Ошибка чтения", ex.Message);
			}

		}

		// Найти все учебные планы по школе
		public Response<IQueryable<CurriculumDto>> GetAllBySchoolId(int schoolId)
		{
			if (SchoolIdIsExist(schoolId))
			{
				try
				{
					return new Response<IQueryable<CurriculumDto>>(_genericRepository.GetAllQueryable().Where(c=>c.SchoolId==schoolId).Select(s=>s.ToDto()));
				}
				catch (Exception ex)
				{
					return new Response<IQueryable<CurriculumDto>>("Ошибка чтения", ex.Message);
				}
			}
			else
			{
				return new Response<IQueryable<CurriculumDto>>("Данные о школе не найдены", $"schoolId = {schoolId}");
			}
		}

		public Response<IQueryable<CurriculumDto>> GetPageBySchoolId(int schoolId, int start,int count)
		{
			if (SchoolIdIsExist(schoolId))
			{
				try
				{
					return new Response<IQueryable<CurriculumDto>>(GetAllQueryable().Value.Where(c => c != null).Where(s => s.SchoolId == schoolId).Skip(start*count).Take(count));
				}
				catch (Exception ex)
				{
					return new Response<IQueryable<CurriculumDto>>("Ошибка чтения", ex.Message);
				}
			}
			else
			{
				return new Response<IQueryable<CurriculumDto>>("Данные о школе не найдены", $"schoolId = {schoolId}");
			}
		}

		// Вспомогательные методы

		// Сохранения в базу данных
		private async Task<Response<CurriculumDto>> SaveChance(Curriculum instance)
		{
			try
			{
				await _unitWork.Commit();
			}
			catch (Exception ex)
			{
				return new Response<CurriculumDto>("Ошибка сохранения в базу данных", ex.Message);
			}
			return new Response<CurriculumDto>(instance.ToDto());
		}
		private bool SchoolIdIsExist(int Id)
		{
			try
			{
				return (_schoolgenericRepository.GetByIdAsync(Id).Result != null);
			}
			catch
			{
				return false;
			}
		}
	}
}
