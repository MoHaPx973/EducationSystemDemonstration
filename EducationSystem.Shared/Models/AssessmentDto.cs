namespace EducationSystem.Shared.Models
{
	public class AssessmentDto
	{
		public int Id { get; set; }
		public int StudentId { get; set; }
		public int TeacherId { get; set; }
		public int SchoolClassId { get; set; }
		public int ItemId { get; set; }
		public DateTime Date { get; set; }
		public int Point { get; set; }

	}
}
