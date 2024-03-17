using EducationSystem.App.Mappers;
using EducationSystem.App.Storage.GenericInterface;
using EducationSystem.Domain.Models;
using EducationSystem.Domain.Relationships;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;

namespace EducationSystem.App.Interactor
{
	public class PersonInteractor
	{
		private IGenericRepository<Person> _genericRepository;
		private IUnitWork _unitWork;
		private IGenericRepository<School> _schoolgenericRepository;
		private IGenericRepository<StudentInClass> _studentInClassgenericRepository;
		public PersonInteractor(IGenericRepository<Person> genericRepository, IUnitWork unitWork, IGenericRepository<School> schoolgenericRepository, IGenericRepository<StudentInClass> studentInClassgenericRepository)
		{
			this._genericRepository = genericRepository;
			this._unitWork = unitWork;
			this._schoolgenericRepository = schoolgenericRepository;
			this._studentInClassgenericRepository = studentInClassgenericRepository;
		}

		// Методы

		// Создание
		public async Task<Response<PersonDto>> Insert(string? name, string? surname, int schoolId,int persontypeId)
		{
			if (SchoolIdIsExist(schoolId))
				{
						Person instance = new Person();
						try
						{
							instance.Name = name;
							instance.Surname = surname;
							instance.SchoolId = schoolId;
							instance.PersonTypeId = persontypeId;
							_genericRepository.Insert(instance);
						}
						catch (NullReferenceException ex)
						{
							return new Response<PersonDto>("Ошибка, данные введены не верно", ex.Message);
						}
						catch (Exception ex)
						{
							return new Response<PersonDto>("Ошибка при записи в базу данных", ex.Message);
						}
						return await SaveChance(instance);
				}
				else return new Response<PersonDto>("Ошибка, данные введены не верно, данной школы не существует", $"schoolId = {schoolId}");
		}

		// Поиск по идентификатору
		public async Task<Response<PersonDto>> GetByIdAsync(int id)
		{
				try
				{
					Person? instance = await _genericRepository.GetByIdAsync(id);
					if (instance == null)
					{
						return new Response<PersonDto>("Учитель не найден", $"id = {id}");
					}
					return new Response<PersonDto>(instance.ToDto());
				}
				catch (Exception ex)
				{
					return new Response<PersonDto>("Ошибка при чтении из базы данных", ex.Message);
				}
		}

		// Обновление данных
		public async Task<Response<PersonDto>> Update(int id, string? name, string? surname, int schoolId)
		{
			Person? instance = new Person();
			try
			{
				instance = await _genericRepository.GetByIdAsync(id);
				if (instance == null) { return new Response<PersonDto>("Ошибка, данные введены не верно, информация об персоне не найдена", $"id={id}"); }
			}
			catch (Exception ex)
			{
				return new Response<PersonDto>("Ошибка, данные введены не верно, персона не найдена", ex.Message);
			}
			try
			{
				instance.Name = name;
				instance.Surname = surname;
				instance.SchoolId = schoolId;
				if (SchoolIdIsExist(instance.SchoolId))
				{
					_genericRepository.Update(instance);
					return await SaveChance(instance);
				}
				else
					return new Response<PersonDto>("Ошибка, данные введены не верно, данной школы не существует", $"schoolId = {schoolId}");
			}
			catch (NullReferenceException ex)
			{
				return new Response<PersonDto>("Данные введены не верное", ex.Message);
			}
			catch (Exception ex)
			{
				return new Response<PersonDto>("Ошибка изменения данных", ex.Message);
			}
		}

		// Удаление
		public async Task<Response<PersonDto>> Delete(int id)
		{
			Person? instance = new Person();
			try
			{
				instance = await _genericRepository.GetByIdAsync(id);
				if (instance == null) { return new Response<PersonDto>("Ошибка, данные введены не верно, информация об учителе не найдена", $"id={id}"); }
			}
			catch (Exception ex)
			{
				return new Response<PersonDto>("Ошибка, данные введены не верно,учитель не найден", ex.Message);
			}
			try
			{
				_genericRepository.Delete(instance);
				return await SaveChance(instance);
			}
			catch (Exception ex)
			{
				return new Response<PersonDto>("Ошибка удаления", ex.Message);

			}
		}

		// Вывод всех данных из списка
		public Response<IQueryable<PersonDto>> GetAllQueryable()
		{
			try
			{
				return new Response<IQueryable<PersonDto>>(_genericRepository.GetAllQueryable().Select(s => s.ToDto()));
			}
			catch (Exception ex)
			{
				return new Response<IQueryable<PersonDto>>("Ошибка чтения", ex.Message);
			}

		}

		public Response<Page<PersonDto>> GetPageQueryable(int start, int count)
		{
			try
			{
				return new Response<Page<PersonDto>>(new Page<PersonDto>(start, count, GetAllQueryable().Value));
			}
			catch (Exception ex)
			{
				return new Response<Page<PersonDto>>("Ошибка чтения", ex.Message);
			}

		}

		//Найти всех учителей по школе
		public Response<IQueryable<PersonDto>> GetAllBySchoolId(int schoolId)
		{
			if (SchoolIdIsExist(schoolId))
			{
				try
				{
					return new Response<IQueryable<PersonDto>>(_genericRepository.GetAllQueryable().Where(c => c.SchoolId == schoolId).Select(s => s.ToDto()));
				}
				catch (Exception ex)
				{
					return new Response<IQueryable<PersonDto>>("Ошибка чтения", ex.Message);
				}
			}
			else
			{
				return new Response<IQueryable<PersonDto>>("Данные об школе" +
					" не найдены", $"schoolId = {schoolId}");
			}
		}

		public Response<IQueryable<PersonDto>> GetPageBySchoolId(int schoolId, int start, int count)
		{
			if (SchoolIdIsExist(schoolId))
			{
				try
				{
					return new Response<IQueryable<PersonDto>>(_genericRepository.GetAllQueryable().Where(c => c != null).Where(s => s.SchoolId == schoolId).Skip(start * count).Take(count).Select(t => t.ToDto()));
				}
				catch (Exception ex)
				{
					return new Response<IQueryable<PersonDto>>("Ошибка чтения", ex.Message);
				}
			}
			else
			{
				return new Response<IQueryable<PersonDto>>("Данные о школе не найдены", $"schoolId = {schoolId}");
			}
		}

		// Все ученики в классе
		public Response<IEnumerable<PersonDto>> GetAllByClassId(int classId)
		{
			try
			{
				List<StudentInClass> list = new List<StudentInClass>();
				list = _studentInClassgenericRepository.GetAllEnumerable().Where(i => i.SchoolClassId == classId).ToList();
				List<PersonDto> studentInClass = new List<PersonDto>();
				foreach (var item in list)
				{
					studentInClass.AddRange(_genericRepository.GetAllEnumerable().Where(i=>i.Id==item.StudentId).Select(d=>d.ToDto()).ToList().OrderBy(s => s.Surname).ThenBy(n => n.Name));
				}
				if (studentInClass.Count != 0)
					return new Response<IEnumerable<PersonDto>>(studentInClass.AsEnumerable());
				else
					return new Response<IEnumerable<PersonDto>>("Список учеников отсутствует","studentInClass count = 0");
			}
			catch (Exception ex)
			{
				return new Response<IEnumerable<PersonDto>>("Ошибка чтения", ex.Message);
			}
		}

		// Вспомогательные методы

		// Сохранения в базу данных
		private async Task<Response<PersonDto>> SaveChance(Person instance)
		{
			try
			{
				await _unitWork.Commit();
			}
			catch (Exception ex)
			{
				return new Response<PersonDto>("Ошибка сохранения в базу данных", ex.Message);
			}
			return new Response<PersonDto>(instance.ToDto());
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
		//private bool ClassIdIsExist(int Id)
		//{
		//	try
		//	{
		//		return (_classlgenericRepository.GetByIdAsync(Id).Result != null);
		//	}
		//	catch
		//	{
		//		return false;
		//	}
		//}
	}
}
