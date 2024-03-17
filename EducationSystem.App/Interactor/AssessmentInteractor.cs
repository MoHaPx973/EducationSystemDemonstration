using EducationSystem.App.Mappers;
using EducationSystem.App.Storage.GenericInterface;
using EducationSystem.Domain.Models;
using EducationSystem.Domain.Relationships;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;
using System.ComponentModel.Design;

namespace EducationSystem.App.Interactor
{
	public class AssessmentInteractor
	{
		private IGenericRepository<Assessment> _genericRepository;
		private IUnitWork _unitWork;
		private IGenericRepository<Person> _persongenericRepository;
		private IGenericRepository<SchoolClass> _classgenericRepository;
		private IGenericRepository<StudentInClass> _studentInClassgenericRepository;
		private IGenericRepository<ItemInCurriculum> _itemInCurriculumGenericRepository;

		public AssessmentInteractor(IGenericRepository<Assessment> genericRepository, IUnitWork unitWork, 
			IGenericRepository<Person> persongenericRepository, IGenericRepository<SchoolClass> classgenericRepository,
			IGenericRepository<StudentInClass> studentInClassgenericRepository, IGenericRepository<ItemInCurriculum> itemInCurriculumGenericRepository)
		{
			_genericRepository = genericRepository;
			_unitWork = unitWork;
			_persongenericRepository = persongenericRepository;
			_classgenericRepository = classgenericRepository;
			_studentInClassgenericRepository = studentInClassgenericRepository;
			_itemInCurriculumGenericRepository = itemInCurriculumGenericRepository;
		}

		// Методы

		// Создание
		public async Task<Response<AssessmentDto>> Insert(int studentId,int teacherId,int schoolClassId, int itemId, DateTime date, int point)
		{
			try
			{
				Assessment assessment = new Assessment(studentId, teacherId, schoolClassId, itemId, date, point);
				Person Student = GetStudentModel(studentId);
				SchoolClass StudentClass = GetSchoolClassModel(schoolClassId);
				CheckRolePerson(Student, teacherId);
				CheckSchoolClass(Student, StudentClass);
				CheckItemInCurriculum(itemId, StudentClass);
				_genericRepository.Insert(assessment);
				return await SaveChance(assessment);
			}

			catch(NullReferenceException ex)
			{
				return new Response<AssessmentDto>("Введите данные", ex.Message);
			}
			catch (ArgumentNullException ex)
			{
				return new Response<AssessmentDto>("Ошибка при заполнении", ex.Message);
			}
			catch (Exception ex)
			{
				return new Response<AssessmentDto>("Ошибка при создании оценки", ex.Message);
			}
		}

		public Response<IEnumerable<AssessmentDto>> GetAllByStudentIdItemId(int studentId, int itemId)
		{
			try
			{
				Person person = GetStudentModel(studentId);
				List<Assessment> assessments = new List<Assessment>();
				return new Response<IEnumerable<AssessmentDto>>
					(_genericRepository.GetAllEnumerable().Where(s => s.StudentId == person.Id).Where(i => i.ItemId == itemId).Select(a=>a.ToDto())); 
			}
			catch
			{
				return new Response<IEnumerable<AssessmentDto>>("Не найдено оценок",$"studentId = {studentId} itemId = {itemId}");
			}
		}

		// Удаление
		public async Task<Response<AssessmentDto>> Delete(int id)
		{
			Assessment? instance = new Assessment();
			try
			{
				instance = await _genericRepository.GetByIdAsync(id);
				if (instance == null) { return new Response<AssessmentDto>("Ошибка, данные введены неверно, информация об оценке не найдена", $"id={id}"); }
			}
			catch (Exception ex)
			{
				return new Response<AssessmentDto>("Ошибка, данные введены неверно, оценка не найдена", ex.Message);
			}
			try
			{
				_genericRepository.Delete(instance);
				return await SaveChance(instance);
			}
			catch (Exception ex)
			{
				return new Response<AssessmentDto>("Ошибка удаления", ex.Message);

			}
		}
			//// Поиск по идентификатору
			//public async Task<Response<AssessmentDto>> GetByIdAsync(int id)
			//{
			//	try
			//	{
			//		Curriculum? instance = await _genericRepository.GetByIdAsync(id);
			//		if (instance == null)
			//		{
			//			return new Response<AssessmentDto>("Учебный план не найден", $"id = {id}");
			//		}
			//		return new Response<AssessmentDto>(instance.ToDto());
			//	}
			//	catch (Exception ex)
			//	{
			//		return new Response<AssessmentDto>("Ошибка при чтении из базы данных", ex.Message);
			//	}
			//}

			//// Обновление данных
			//public async Task<Response<AssessmentDto>> Update(int id, int schoolId, int yearformation)
			//{
			//	Curriculum? instance = new Curriculum();
			//	try
			//	{
			//		instance = await _genericRepository.GetByIdAsync(id);
			//		if (instance == null) { return new Response<AssessmentDto>("Ошибка, данные введены не верно, информация об учебном плане  не найдена", $"id={id}"); }
			//	}
			//	catch (Exception ex)
			//	{
			//		return new Response<AssessmentDto>("Ошибка, данные введены не верно, учебный план не найден", ex.Message);
			//	}
			//	try
			//	{
			//		instance.SchoolId = schoolId;
			//		instance.YearFormation = yearformation;
			//		if (SchoolIdIsExist(instance.SchoolId))
			//		{
			//			_genericRepository.Update(instance);
			//			return await SaveChance(instance);
			//		}
			//		else return new Response<AssessmentDto>("Ошибка, данные введены не верно, данной школы не существует", $"schoolId = {schoolId}");
			//	}
			//	catch (Exception ex)
			//	{
			//		return new Response<AssessmentDto>("Ошибка изменения данных", ex.Message);
			//	}
			//}


			//// Вывод всех данных из списка
			//public Response<IQueryable<AssessmentDto>> GetAllQueryable()
			//{
			//	try
			//	{
			//		return new Response<IQueryable<AssessmentDto>>(_genericRepository.GetAllQueryable().Select(s => s.ToDto()));
			//	}
			//	catch (Exception ex)
			//	{
			//		return new Response<IQueryable<AssessmentDto>>("Ошибка чтения", ex.Message);
			//	}

			//}

			//public Response<Page<AssessmentDto>> GetPageQueryable(int start, int count)
			//{
			//	try
			//	{
			//		return new Response<Page<AssessmentDto>>(new Page<CurriculumDto>(start, count, GetAllQueryable().Value));
			//	}
			//	catch (Exception ex)
			//	{
			//		return new Response<Page<AssessmentDto>>("Ошибка чтения", ex.Message);
			//	}

			//}

			//// Найти все учебные планы по школе
			//public Response<IQueryable<AssessmentDto>> GetAllBySchoolId(int schoolId)
			//{
			//	if (SchoolIdIsExist(schoolId))
			//	{
			//		try
			//		{
			//			return new Response<IQueryable<AssessmentDto>>(_genericRepository.GetAllQueryable().Where(c=>c.SchoolId==schoolId).Select(s=>s.ToDto()));
			//		}
			//		catch (Exception ex)
			//		{
			//			return new Response<IQueryable<AssessmentDto>>("Ошибка чтения", ex.Message);
			//		}
			//	}
			//	else
			//	{
			//		return new Response<IQueryable<AssessmentDto>>("Данные о школе не найдены", $"schoolId = {schoolId}");
			//	}
			//}

			//public Response<IQueryable<AssessmentDto>> GetPageBySchoolId(int schoolId, int start,int count)
			//{
			//	if (SchoolIdIsExist(schoolId))
			//	{
			//		try
			//		{
			//			return new Response<IQueryable<AssessmentDto>>(GetAllQueryable().Value.Where(c => c != null).Where(s => s.SchoolId == schoolId).Skip(start*count).Take(count));
			//		}
			//		catch (Exception ex)
			//		{
			//			return new Response<IQueryable<AssessmentDto>>("Ошибка чтения", ex.Message);
			//		}
			//	}
			//	else
			//	{
			//		return new Response<IQueryable<AssessmentDto>>("Данные о школе не найдены", $"schoolId = {schoolId}");
			//	}
			//}

			// Вспомогательные методы

			// Сохранения в базу данных
			private async Task<Response<AssessmentDto>> SaveChance(Assessment instance)
		{
			try
			{
				await _unitWork.Commit();
			}
			catch (Exception ex)
			{
				return new Response<AssessmentDto>("Ошибка сохранения в базу данных", ex.Message);
			}
			return new Response<AssessmentDto>(instance.ToDto());
		}
		
		private Person GetStudentModel(int studentId)
		{
			try
			{
				return _persongenericRepository.GetByIdAsync(studentId).Result;
			}
			catch
			{
				throw new NullReferenceException("Ученик не найден");
			}
		}
		private SchoolClass GetSchoolClassModel(int schoolclassId)
		{
			try
			{
				return _classgenericRepository.GetByIdAsync(schoolclassId).Result;
			}
			catch
			{
				throw new NullReferenceException("Класс не найден");
			}
		}

		private void CheckRolePerson(Person Student, int teacherId)
		{
			if ((Student.PersonTypeId == 1) && (_persongenericRepository.GetByIdAsync(teacherId).Result.PersonTypeId == 2)) ;
			else throw new ArgumentNullException("Ошибка, информация об ученике или учителе введена неверно");

		}

		private void CheckSchoolClass(Person Student, SchoolClass schoolClass)
		{
			if (((_studentInClassgenericRepository.CombinedKey(Student.Id, schoolClass.Id)).Result != null)) ;
			else throw new ArgumentNullException("Ошибка, информация о классе введена неверно");
		}
		private void CheckItemInCurriculum(int itemId, SchoolClass schoolClass)
		{
			if ((_itemInCurriculumGenericRepository.CombinedKey(itemId, schoolClass.CurriculumId)).Result != null) ;
			else throw new ArgumentNullException("Ошибка, информация об учебном плане введена неверно");
		}

	}
}
