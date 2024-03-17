using EducationSystem.App.Mappers;
using EducationSystem.App.Storage.GenericInterface;
using EducationSystem.Domain.Models;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;
using System.Collections.Generic;
using System.Xml.Linq;


namespace EducationSystem.App.Interactor
{
	public class SchoolClassInteractor
	{
		private IGenericRepository<SchoolClass> _genericRepository;
		private IUnitWork _unitWork;
		private IGenericRepository<School> _schoolgenericRepository;
		private IGenericRepository<Curriculum> _curriculumgenericRepository;
		private PersonInteractor _personInteractor;
		private StudentInClassInteractor _studentInClassInteractor;
		public SchoolClassInteractor(IGenericRepository<SchoolClass> genericRepository, IUnitWork unitWork, 
			IGenericRepository<School> schoolgenericRepository, IGenericRepository<Curriculum> curriculumgenericRepository, PersonInteractor personInteractor, StudentInClassInteractor studentInClassInteractor)
		{
			this._genericRepository = genericRepository;
			this._unitWork = unitWork;
			this._schoolgenericRepository = schoolgenericRepository;
			this._curriculumgenericRepository = curriculumgenericRepository;
			this._personInteractor = personInteractor;
			this._studentInClassInteractor = studentInClassInteractor;

        }

		// Методы


		// Создание
		public async Task<Response<SchoolClassDto>> Insert(int schoolId, int number,string? letter, int curriculumId, int year)
		{
			if (SchoolIdIsExist(schoolId))
			{
				if (CurriculumIdIsExist(curriculumId))
				{
						try
						{
							SchoolClass instance = new SchoolClass() { SchoolId = schoolId, Number = number, Letter = letter, CurriculumId = curriculumId, YearFormation = year };
							_genericRepository.Insert(instance);
							return await SaveChance(instance);
						}
						catch (NullReferenceException ex)
						{
							return new Response<SchoolClassDto>("Ошибка, данные введены не верно", ex.Message);
						}
						catch (Exception ex)
						{
							return new Response<SchoolClassDto>("Ошибка при записи в базу данных", ex.Message);
						}
				}
				else return new Response<SchoolClassDto>("Ошибка, данные введены не верно, данного учебного плана не существует", $"curriculumId = {curriculumId}");
			}
			else return new Response<SchoolClassDto>("Ошибка, данные введены не верно, данной школы не существует", $"schoolId = {schoolId}");
		}

		// Поиск по идентификатору
		public async Task<Response<SchoolClassDto>> GetByIdAsync(int id)
		{
				try
				{
					SchoolClass? instance = await _genericRepository.GetByIdAsync(id);
					return new Response<SchoolClassDto>(instance.ToDto());
				}
				catch (Exception ex)
				{
					return new Response<SchoolClassDto>("Ошибка класс не найден", ex.Message);
				}
		}

		// Обновление данных
		public async Task<Response<SchoolClassDto>> Update(int id,int schoolId, int number, string? letter, int curriculumId, int year)
		{
			SchoolClass? instance = new SchoolClass();
			try
			{
				instance = await _genericRepository.GetByIdAsync(id);
				if (instance == null) { return new Response<SchoolClassDto>("Ошибка, данные введены не верно, информация об классе не найдена", $"id={id}"); }
			}
			catch (Exception ex)
			{
				return new Response<SchoolClassDto>("Ошибка, данные введены не верно, класс не найден", ex.Message);
			}

			try
			{
				instance.SchoolId = schoolId;
				instance.Number = number;
				instance.Letter = letter;
				instance.CurriculumId = curriculumId;
				instance.YearFormation = year;
				if (SchoolIdIsExist(instance.SchoolId))
				{
					if (CurriculumIdIsExist(instance.CurriculumId))
					{
						_genericRepository.Update(instance);
						return await SaveChance(instance);
					}
					else return new Response<SchoolClassDto>("Ошибка, данные введены не верно, данного учебного плана не существует", $"curriculumId = {curriculumId}");

				}
				else return new Response<SchoolClassDto>("Ошибка, данные введены не верно, данной школы не существует", $"schoolId = {schoolId}");
			}
			catch (Exception ex)
			{
				return new Response<SchoolClassDto>("Ошибка изменения данных", ex.Message);
			}
		}

		// Удаление
		public async Task<Response<SchoolClassDto>> Delete(int id)
		{
			if (!ClassNotReferenced(id)) return new Response<SchoolClassDto>("Ошибка удаления, в классе учатся ученики", $"classId = {id}"); 
			SchoolClass? instance = new SchoolClass();
			try
			{
				instance = await _genericRepository.GetByIdAsync(id);
				if (instance == null) { return new Response<SchoolClassDto>("Ошибка, данные введены не верно, информация о классе не найдена", $"id={id}"); }
			}
			catch (Exception ex)
			{
				return new Response<SchoolClassDto>("Ошибка, данные введены не верно,класс не найден", ex.Message);
			}
			try
			{
				_genericRepository.Delete(instance);
				return await SaveChance(instance);
			}
			catch (Exception ex)
			{
				return new Response<SchoolClassDto>("Ошибка удаления", ex.Message);

			}
		}

		// Вывод всех данных из списка
		public async Task<Response<SchoolClassDto>> GetClassByStudentId(int studentId)
		{
			Response<IEnumerable<StudentInClassDto>> studentInClass = new();
            try
			{
				studentInClass = _studentInClassInteractor.GetAllEnumerable();
				if (studentInClass == null)
                    throw new Exception("Ошибка поиска");
                if (studentInClass.Value==null)
					throw new Exception("Ошибка поиска");
                return await GetByIdAsync(studentInClass.Value.Where(c => c.StudentId == studentId).ToList()[0].SchoolClassId);
			}
            catch (Exception ex)
			{
				return new Response<SchoolClassDto>("Класс студента не найден", ex.Message);
			}
		}

        public Response<IQueryable<SchoolClassDto>> GetAllQueryable()
		{
			try
			{
				return new Response<IQueryable<SchoolClassDto>>(_genericRepository.GetAllQueryable().Select(s => s.ToDto()));
			}
			catch (Exception ex)
			{
				return new Response<IQueryable<SchoolClassDto>>("Ошибка чтения", ex.Message);
			}

		}

		public Response<IEnumerable<SchoolClassDto>> GetPageEnumerable(int start, int count)
		{
			try
			{
				Page<SchoolClass> page = new Page<SchoolClass>(start, count, _genericRepository.GetAllEnumerable().OrderBy(n=>n.Number).ThenBy(l=>l.Letter));
				return new Response<IEnumerable<SchoolClassDto>>(page.Items.Select(s => s.ToDto()));
			}
			catch (Exception ex)
			{
				return new Response<IEnumerable<SchoolClassDto>>("Ошибка чтения", ex.Message);
			}
		}
		public Response<IEnumerable<SchoolClassDto>> GetPageEnumerableByYear(int start, int count,int year)
		{
			try
			{
				Page<SchoolClass> page = new Page<SchoolClass>(start, count, _genericRepository.GetAllEnumerable().Where(y=>y.YearFormation==year).OrderBy(n => n.Number).ThenBy(l => l.Letter));
				return new Response<IEnumerable<SchoolClassDto>>(page.Items.Select(s => s.ToDto()));
			}
			catch (Exception ex)
			{
				return new Response<IEnumerable<SchoolClassDto>>("Ошибка чтения", ex.Message);
			}
		}

		// Найти все учебные планы по школе
		public Response<IQueryable<SchoolClassDto>> GetAllBySchoolId(int schoolId)
		{
			if (SchoolIdIsExist(schoolId))
			{
				try
				{
					return new Response<IQueryable<SchoolClassDto>>(_genericRepository.GetAllQueryable().Where(c=>c.SchoolId==schoolId).Select(s=>s.ToDto()));
				}
				catch (Exception ex)
				{
					return new Response<IQueryable<SchoolClassDto>>("Ошибка чтения", ex.Message);
				}
			}
			else
			{
				return new Response<IQueryable<SchoolClassDto>>("Данные о школе не найдены", $"schoolId = {schoolId}");
			}
		}

		public Response<IQueryable<SchoolClassDto>> GetPageBySchoolId(int schoolId, int start,int count)
		{
			if (SchoolIdIsExist(schoolId))
			{
				try
				{
					return new Response<IQueryable<SchoolClassDto>>(_genericRepository.GetAllQueryable().Where(c => c != null).Where(s => s.SchoolId == schoolId).Skip(start*count).Take(count).Select(t=>t.ToDto()));
				}
				catch (Exception ex)
				{
					return new Response<IQueryable<SchoolClassDto>>("Ошибка чтения", ex.Message);
				}
			}
			else
			{
				return new Response<IQueryable<SchoolClassDto>>("Данные о школе не найдены", $"schoolId = {schoolId}");
			}
		}
		
		//public Response<IQueryable<SchoolClassDto>> GetAllByCurriculum(int curriculumId)
		//{
		//	if (CurriculumIdIsExist(curriculumId))
		//	{
		//		try
		//		{
		//			return new Response<IQueryable<SchoolClassDto>>(_genericRepository.GetAllQueryable().Where(c => c.CurriculumId == curriculumId).Select(s => s.ToDto()));
		//		}
		//		catch (Exception ex)
		//		{
		//			return new Response<IQueryable<SchoolClassDto>>("Ошибка чтения", ex.Message);
		//		}
		//	}
		//	else
		//	{
		//		return new Response<IQueryable<SchoolClassDto>>("Данные о школе не найдены", $"curriculumId = {curriculumId}");
		//	}
		//}

		//public Response<IQueryable<SchoolClassDto>> GetPageByCurriculumId(int curriculumId, int start, int count)
		//{
		//	if (CurriculumIdIsExist(curriculumId))
		//	{
		//		try
		//		{
		//			return new Response<IQueryable<SchoolClassDto>>(_genericRepository.GetAllQueryable().Where(c => c != null).Where(s => s.CurriculumId == curriculumId).Skip(start * count).Take(count).Select(t => t.ToDto()));
		//		}
		//		catch (Exception ex)
		//		{
		//			return new Response<IQueryable<SchoolClassDto>>("Ошибка чтения", ex.Message);
		//		}
		//	}
		//	else
		//	{
		//		return new Response<IQueryable<SchoolClassDto>>("Данные о школе не найдены", $"curriculumId = {curriculumId}");
		//	}
		//}

		// Вспомогательные методы

		// Сохранения в базу данных
		private async Task<Response<SchoolClassDto>> SaveChance(SchoolClass instance)
		{
			try
			{
				await _unitWork.Commit();
			}
			catch (Exception ex)
			{
				return new Response<SchoolClassDto>("Ошибка сохранения в базу данных", ex.Message);
			}
			return new Response<SchoolClassDto>(instance.ToDto());
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
		private bool CurriculumIdIsExist(int Id)
		{
			try
			{
				return (_curriculumgenericRepository.GetByIdAsync(Id).Result != null);
			}
			catch
			{
				return false;
			}
		}
		private bool ClassNotReferenced(int id)
		{
			return (!_personInteractor.GetAllByClassId(id).Value.Any());
		}
	}
}
