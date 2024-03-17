namespace EducationSystem.Domain.Models
{
	public class Curriculum
	{
        public int Id { get; set; }
        public int SchoolId
		{
			get => _schoolId;
			set
			{
				if (!(_schoolId == 0) && (value == 0))
				{
					return;
				}
				if (value == 0)
				{
					throw new NullReferenceException("Ошибка заполнения школы");
				}
				_schoolId = value;
			}
		}
		public int YearFormation
		{
			get => _year;
			set
			{
				if (!(_year == 0) && (value == 0))
				{
					return;
				}
				if (value == 0)
				{
					throw new NullReferenceException("Ошибка заполнения года");
				}
				_year = value;
			}
		}

		private int _schoolId;
		private int _year;
	}
}