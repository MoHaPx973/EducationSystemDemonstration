using EducationSystem.App.Mappers;
using EducationSystem.App.Storage.GenericInterface;
using EducationSystem.Domain.Models;
using EducationSystem.Domain.Relationships;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;
using System.Runtime.CompilerServices;

namespace EducationSystem.App.Interactor
{
    public class StudentInClassInteractor
	{
		private IGenericRepository<StudentInClass> _genericRepository;
		private IUnitWork _unitWork;
		private IGenericRepository<Person> _personGenericRepository;
		private IGenericRepository<SchoolClass> _classGenericRepository;

		public StudentInClassInteractor(IGenericRepository<StudentInClass> genericRepository, IUnitWork unitWork, IGenericRepository<Person> personGenericRepository, IGenericRepository<SchoolClass> classGenericRepository)
		{
			_genericRepository = genericRepository;
			_unitWork = unitWork;
			_personGenericRepository = personGenericRepository;
			_classGenericRepository = classGenericRepository;
		}

		// Методы

		// Создание
		public async Task<Response<StudentInClassDto>> Insert(int studentId,int classId)
		{
			if (ClassInSchool(studentId,classId))
			{
				StudentInClass instance = new StudentInClass();
				try
				{
					instance.StudentId= studentId;
					instance.SchoolClassId = classId;
					_genericRepository.Insert(instance);
				}
				catch(NullReferenceException ex)
				{
					return new Response<StudentInClassDto>("Данные не введены", ex.Message);
				}
				catch (Exception ex)
				{
					return new Response<StudentInClassDto>("Ошибка при записи в базу данных", ex.Message);
				}
					return await SaveChance(instance);
			}
				else
			{
				return new Response<StudentInClassDto>("Ошибка, данные введены не верно", $"studentId = {studentId} classId={classId}");
			}
		}

		// Поиск по идентификатору
		public async Task<Response<StudentInClassDto>> GetByIdAsync(int studentId, int schoolClassId)
		{
			try
			{
				StudentInClass? instance = await _genericRepository.CombinedKey(studentId, schoolClassId);
				if (instance == null)
				{
					return new Response<StudentInClassDto>("Связь не найдена", $"studentId = {studentId} schoolClassId={schoolClassId}");
				}
				return new Response<StudentInClassDto>(instance.ToDto());
			}
			catch (Exception ex)
			{
				return new Response<StudentInClassDto>("Ошибка при чтении из базы данных", ex.Message);
			}
		}

		// Обновление данных - не работает
		public async Task<Response<StudentInClassDto>> Update(int id, int studentId, int classId)

		{
			StudentInClass? instance = new StudentInClass();
			try
			{
				instance = await _genericRepository.GetByIdAsync(id);
				if (instance == null) { return new Response<StudentInClassDto>("Ошибка, данные введены не верно, информация об учебном плане  не найдена", $"id={id}"); }
			}
			catch (Exception ex)
			{
				return new Response<StudentInClassDto>("Ошибка, данные введены не верно, учебный план не найден", ex.Message);
			}
			try
			{
				instance.StudentId = studentId;
				instance.SchoolClassId = classId;
				if (ClassInSchool(studentId, classId))
				{
					_genericRepository.Update(instance);
					return await SaveChance(instance);
				}
				return new Response<StudentInClassDto>("Ошибка, данные введены не верно", $"studentId = {studentId} classId={classId}");
			}
			catch (Exception ex)
			{
				return new Response<StudentInClassDto>("Ошибка изменения данных", ex.Message);
			}
		}

		// Удаление
		public async Task<Response<StudentInClassDto>> Delete(int personId, int classId)
		{
			StudentInClass? instance = new StudentInClass();
			try
			{
				instance = await _genericRepository.CombinedKey(personId, classId);
				if (instance == null) { return new Response<StudentInClassDto>("Ошибка, данные введены не верно, информация об свзяи не найдена", $"id={personId}"); }
			}
			catch (Exception ex)
			{
				return new Response<StudentInClassDto>("Ошибка, данные введены не верно, учебный план не найден", ex.Message);
			}
			try
			{
				_genericRepository.Delete(instance);
				return await SaveChance(instance);
			}
			catch (Exception ex)
			{
				return new Response<StudentInClassDto>("Ошибка удаления", ex.Message);

			}
		}

		// Вывод всех данных из списка
		public Response<IEnumerable<StudentInClassDto>> GetAllEnumerable()
		{
			try
			{
				return new Response<IEnumerable<StudentInClassDto>>(_genericRepository.GetAllEnumerable().Select(s => s.ToDto()));
			}
			catch (Exception ex)
			{
				return new Response<IEnumerable<StudentInClassDto>>("Ошибка чтения", ex.Message);
			}

		}

		
		// Вспомогательные методы

		// Сохранения в базу данных
		private async Task<Response<StudentInClassDto>> SaveChance(StudentInClass instance)
		{
			try
			{
				await _unitWork.Commit();
			}
			catch (Exception ex)
			{
				return new Response<StudentInClassDto>("Ошибка сохранения в базу данных", ex.Message);
			}
			return new Response<StudentInClassDto>(instance.ToDto());
		}

		private bool ClassInSchool(int personId,int classId)
		{
			try
			{
				Person student = _personGenericRepository.GetByIdAsync(personId).Result;
				SchoolClass schoolClass = _classGenericRepository.GetByIdAsync(classId).Result;
				if ((schoolClass == null)&&(student == null))	return false;
				if (!(student.PersonTypeId == 1)) return false;
				if (student.SchoolId == schoolClass.SchoolId) return true;
				else return false;
			}
			catch
			{
				return false;
			}
			
		}
	}
}
