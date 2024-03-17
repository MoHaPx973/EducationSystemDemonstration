namespace EducationSystem.Shared.Models
{
	public class SchoolClassDto
	{
		public int Id { get; set; }
		public int SchoolId { get; set; } // школа
		public int Number { get; set; }
		public string Letter { get; set; } = string.Empty;
		public int CurriculumId { get; set; } // учебный план
		public int YearFormation { get; set; }
	}
}
