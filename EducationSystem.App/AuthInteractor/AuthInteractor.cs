using EducationSystem.App.Mappers;
using EducationSystem.App.Storage.AuthInterface;
using EducationSystem.App.Storage.GenericInterface;
using EducationSystem.Domain.AuthModels;
using EducationSystem.Domain.Models;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;
using System.ComponentModel.DataAnnotations;
using EducationSystem.Provider.AuthenticationToken;


namespace EducationSystem.App.AuthInteractor
{
	public class AuthInteractor
	{
		private IGenericRepository<User> _genericRepository;
		public IAuthRepository _authRepository;
		private IUnitWork _unitWork;
		private IGenericRepository<Person> _persongenericRepository;
		private IAuthenticationService _authservice;

		public AuthInteractor(IGenericRepository<User> genericRepository, IUnitWork unitWork, 
			IGenericRepository<Person> persongenericRepository, IAuthRepository authRepository, IAuthenticationService authservice)
		{
			_genericRepository = genericRepository;
			_unitWork = unitWork;
			_persongenericRepository = persongenericRepository;
			_authRepository = authRepository;
			_authservice = authservice;
		}



		// Методы

		// Создание
		public async Task<Response<UserDto>> Insert(string login, string password,string role,int personId)
		{
			if (PersonlIdIsExist(personId))
			{
				User instance = new User();
				try
				{
					instance.Login = login;
					instance.Password=password;
					instance.Role=role;
					instance.PersonId=personId;
					_genericRepository.Insert(instance);
				}
				catch (NullReferenceException ex)
				{
					return new Response<UserDto>("Ошибка, данные введены не верно", ex.Message);
				}
				catch (Exception ex)
				{
					return new Response<UserDto>("Ошибка при записи в базу данных", ex.Message);
				}
				return await SaveChance(instance);
			}
			else return new Response<UserDto>("Ошибка, данные введены не верно, данного пользователя не существует", $"personId = {personId}");
		}

		// Поиск по идентификатору
		public async Task<Response<UserDto>> GetByIdAsync(int id)
		{
			try
			{
				User? instance = await _genericRepository.GetByIdAsync(id);
				if (instance == null)
				{
					return new Response<UserDto>("Пользователь не найден", $"id = {id}");
				}
				return new Response<UserDto>(instance.ToDto());
			}
			catch (Exception ex)
			{
				return new Response<UserDto>("Ошибка при чтении из базы данных", ex.Message);
			}
		}

        public async Task<Response<UserDto>> GetByLoginAsync(string login)
        {
            try
            {
                User? instance = await _authRepository.GetByName(login);
                if (instance == null)
                {
                    return new Response<UserDto>("Пользователь не найден", $"login = {login}");
                }
				Person person = _persongenericRepository.GetByIdAsync(instance.PersonId).Result;
                instance.Login = $"{person.Surname} {person.Name}";
				instance.Password = "hidden";
                return new Response<UserDto>(instance.ToDto());
            }
            catch (Exception ex)
            {
                return new Response<UserDto>("Ошибка при чтении из базы данных", ex.Message);
            }
        }

        // Обновление данных
        public async Task<Response<UserDto>> Update(int id, string login, string password, string role, int personId)
		{
			User? instance = new User();
			try
			{
				instance = await _genericRepository.GetByIdAsync(id);
				if (instance == null) { return new Response<UserDto>("Ошибка, данные введены неверно, информация о пользователе не найдена", $"id={id}"); }
			}
			catch (Exception ex)
			{
				return new Response<UserDto>("Ошибка, данные введены не верно, учителе не найден", ex.Message);
			}
			try
			{
				instance.Login = login;
				instance.Password = password;
				instance.Role = role;
				instance.PersonId = personId;
				if (PersonlIdIsExist(instance.PersonId))
				{
					_genericRepository.Update(instance);
					return await SaveChance(instance);
				}
				else
					return new Response<UserDto>("Ошибка, данные введены неверно, данного пользователя не существует", $"personId = {personId}");
			}
			catch (NullReferenceException ex)
			{
				return new Response<UserDto>("Данные введены неверное", ex.Message);
			}
			catch (Exception ex)
			{
				return new Response<UserDto>("Ошибка изменения данных", ex.Message);
			}
		}

		// Удаление
		public async Task<Response<UserDto>> Delete(int id)
		{
			User? instance = new User();
			try
			{
				instance = await _genericRepository.GetByIdAsync(id);
				if (instance == null) { return new Response<UserDto>("Ошибка, данные введены неверно, информация об учителе не найдена", $"id={id}"); }
			}
			catch (Exception ex)
			{
				return new Response<UserDto>("Ошибка, данные введены неверно,учитель не найден", ex.Message);
			}
			try
			{
				_genericRepository.Delete(instance);
				return await SaveChance(instance);
			}
			catch (Exception ex)
			{
				return new Response<UserDto>("Ошибка удаления", ex.Message);

			}
		}

		// Вывод всех данных из списка
		public Response<IQueryable<UserDto>> GetAllQueryable()
		{
			try
			{
				return new Response<IQueryable<UserDto>>(_genericRepository.GetAllQueryable().Select(s => s.ToDto()));
			}
			catch (Exception ex)
			{
				return new Response<IQueryable<UserDto>>("Ошибка чтения", ex.Message);
			}

		}


		// Вспомогательные методы

		// Сохранения в базу данных
		private async Task<Response<UserDto>> SaveChance(User instance)
		{
			try
			{
				await _unitWork.Commit();
			}
			catch (Exception ex)
			{
				return new Response<UserDto>("Ошибка сохранения в базу данных", ex.Message);
			}
			return new Response<UserDto>(instance.ToDto());
		}
		private bool PersonlIdIsExist(int Id)
		{
			try
			{
				return (_persongenericRepository.GetByIdAsync(Id).Result != null);
			}
			catch
			{
				return false;
			}
		}

		public async Task<Response<string>> AuthenticateAsync(string? login, string? password)
		{
			try
			{
				if ((string.IsNullOrWhiteSpace(login))&&(string.IsNullOrWhiteSpace(password)))
				{
					throw new ValidationException("Не все поля были заполнены");
				}

				var user = await _authRepository.GetByName(login);

				if (user == null)
				{
					throw new NullReferenceException("Данные введены не верно");
				}

				var authenticationResult = _authservice.Authenticate(login, password,user.Password,user.Role);

				if (authenticationResult == null)
				{
					throw new ValidationException("Данные введены не верно");
				}

				return new Response<string>(authenticationResult);
			}
			catch (NullReferenceException ex)
			{
				return new Response<string>("",ex.Message);
			}
			catch (Exception ex)
			{
				return new Response<string>("", ex.Message);
			}
		}
	}
}
