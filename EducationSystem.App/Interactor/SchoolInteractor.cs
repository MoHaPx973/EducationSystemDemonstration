using EducationSystem.App.Mappers;
using EducationSystem.App.Storage.GenericInterface;
using EducationSystem.Domain.Models;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;
using System.Linq.Expressions;

namespace EducationSystem.App.Interactor
{
	public class SchoolInteractor
	{
		private IGenericRepository<School> _genericRepository;
		private IUnitWork _unitWork;

		private CurriculumInteractor _curriculumInteractor;
		private PersonInteractor _personInteractor;
		public SchoolInteractor(IGenericRepository<School> genericRepository,IUnitWork unitWork
			,CurriculumInteractor curriculumInteractor, PersonInteractor teacherInteractor) 
		{
			this._genericRepository = genericRepository;
			this._unitWork = unitWork;
			this._curriculumInteractor = curriculumInteractor;
			this._personInteractor = teacherInteractor;
		}

		// Методы

		// Создание
		public async Task<Response<SchoolDto>> Insert(string? name,string? city)
		{
				School instance = new School();
				try
				{
					instance.Name = name;
					instance.City = city;
					_genericRepository.Insert(instance);
				}
				catch (NullReferenceException ex)
				{
					return new Response<SchoolDto>("Ошибка, данные введены не верно", ex.Message);
				}
				catch(Exception ex)
				{
					return new Response<SchoolDto>("Ошибка при записи в базу данных",ex.Message); 
				}
				return await SaveChance(instance);
		}
	
		// Поиск по идентификатору
		public async Task<Response<SchoolDto>> GetByIdAsync(int id)
		{
				try 
				{
					School? instance = await _genericRepository.GetByIdAsync(id);
                    if (instance==null)
                    {
						return new Response<SchoolDto>("Школа не найдена", $"id = {id}"); 
					}
                    return new Response<SchoolDto> (instance.ToDto());
				}
				catch(Exception ex)
				{
					return new Response<SchoolDto> ("Ошибка при чтении из базы данных", ex.Message);
				}
		}

		// Обновление данных
		public async Task<Response<SchoolDto>> Update(int id, string? name, string? city)
		{
			School? instance = new School();
			try
			{
				instance = await _genericRepository.GetByIdAsync(id);
				if (instance == null) { return  new Response<SchoolDto> ("Ошибка, данные введены не верно, информация о школе не найдена", $"id={id}"); }
			}
			catch(Exception ex)
			{
				return new Response<SchoolDto> ("Ошибка, данные введены не верно, школа не найдена", ex.Message);
			}
			try
			{
				instance.Name = name;
				instance.City = city;
				_genericRepository.Update(instance);
				return await SaveChance(instance);
			}
			catch(Exception ex)
			{
				return new Response<SchoolDto> ("Ошибка изменения данных", ex.Message);
			}
		}

		// Удаление
		public async Task<Response<SchoolDto>> Delete(int id)
		{
			School? instance = new School();
			try
			{
				instance = await _genericRepository.GetByIdAsync(id);
				if (instance == null) { return new Response<SchoolDto> ("Ошибка, данные введены не верно, информация о школе не найдена", $"id={id}"); }
			}
			catch (Exception ex)
			{
				return new Response<SchoolDto> ("Ошибка, данные введены не верно, школа не найдена", ex.Message);
			}
			if (SchoolNotReferenced(id))
			{
				try
				{
					_genericRepository.Delete(instance);
					return await SaveChance(instance);
				}
				catch (Exception ex)
				{
					return new Response<SchoolDto>("Ошибка удаления", ex.Message);

				}
			}
			return new Response<SchoolDto>("Ошибка удаления, к школе записаны учебные планы или педагогический состав и обучающиеся", "Curriculum or Person has this.Id"); ;
		}


		// Дополнительные методы

		// Вывод всех данных из списка
		public Response<IEnumerable<SchoolDto>> GetAllEnumerable()
		{
			try
			{
				return new Response<IEnumerable<SchoolDto>>(_genericRepository.GetAllEnumerable().Select(s=>s.ToDto())); 
			}
			catch (Exception ex)
			{
				return new Response<IEnumerable<SchoolDto>> ("Ошибка чтения", ex.Message);
			} 
		}

        public Response<IEnumerable<SchoolDto>> GetPageEnumerable(int start, int count)
		{
			try
			{
				Page<School> page = new Page<School>(start,count,_genericRepository.GetAllEnumerable()); 
				return new Response<IEnumerable<SchoolDto>>(page.Items.Select(s=>s.ToDto()));
			}
			catch (Exception ex)
			{
				return new Response<IEnumerable<SchoolDto>>("Ошибка чтения", ex.Message);
			}
		}



		// Вспомогательные методы

		// Сохранения в базу данных
		private async Task<Response<SchoolDto>> SaveChance(School newschool)
		{
			try
			{
				await _unitWork.Commit();
			}
			catch(Exception ex)
			{
				return new Response<SchoolDto> ("Ошибка сохранения в базу данных", ex.Message);
			}
			return new Response<SchoolDto>(newschool.ToDto()); 
		}

		// Проверка на удаление Школы
		private bool SchoolNotReferenced(int id)
		{
			return (!_curriculumInteractor.GetAllBySchoolId(id).Value.Any()) && (!_personInteractor.GetAllBySchoolId(id).Value.Any());
		}
	}
}
