using EducationSystem.App.AuthInteractor;
using EducationSystem.App.Interactor;
using EducationSystem.Domain.AuthModels;
using EducationSystem.Domain.Models;
using EducationSystem.Provider.Authentication;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EducationSystem.Api.Controllers
{
	[AllowAnonymous]
	[ApiController]
	[Route("[controller]")]
	public class AuthController : Controller
	{
		private AuthInteractor _authInteractor;
		public AuthController(AuthInteractor authInteractor)
		{
			this._authInteractor = authInteractor;
		}

		[HttpPost("Create")]
		public async Task<Response<UserDto>> Insert(UserDto user)
		{
			return await _authInteractor.Insert(user.Login, user.Password, user.Role, user.PersonId);
		}

		[HttpGet("FindById")]
		public async Task<Response<UserDto>> FindById(int id)
		{
			return await _authInteractor.GetByIdAsync(id);
		}
        [HttpGet("FindByName")]
        public async Task<Response<UserDto>> FindByLogin(string login)
        {
            return await _authInteractor.GetByLoginAsync(login);
        }
        [HttpGet("AuthenticateAsync")]
		public async Task<Response<string>> AuthenticateAsync(string? login, string? password)
		{
			return await _authInteractor.AuthenticateAsync(login,password);
		}
	}
}
