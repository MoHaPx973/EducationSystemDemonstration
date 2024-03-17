namespace EducationSystem.Shared.Models
{
	public class UserDto
	{
		public int Id { get; set; }
		public string Login { get; set; } = string.Empty;
		public string Password { get; set; } = string.Empty;
		public string Role { get; set; } = string.Empty;
		public int PersonId { get; set; }

	}
}
