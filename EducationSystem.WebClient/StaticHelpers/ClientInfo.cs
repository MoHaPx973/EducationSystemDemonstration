using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace EducationSystem.WebClient.StaticHelpers
{
    static public class ClientInfo
    {
        static public HttpClient Http { get; set; } = new();
        static public string? UserName { get; set; }
        static public string? UserRole { get; set; }
        static public int? PersonId { get; set; }
        static public bool IsAuthenticated { get; set; }
        static public void Login(string token)
        {
            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        static public void Logout() 
        {
            Http.Dispose();
            UserName= null;
            UserRole= null;
            IsAuthenticated = false;
        }

        static public async Task AddClientInfo(UserDto user)
        {
            UserName = user.Login;
            UserRole = user.Role;
            PersonId = user.PersonId;
            IsAuthenticated = true;
        }

    }
}
