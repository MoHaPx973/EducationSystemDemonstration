namespace EducationSystem.Domain.Models
{
	public class SchoolClass
	{
        public int Id { get; set; }
        public int Number 
		{ 
			get=>_number;
			set
			{
				if (!(_number==0)&&(value==0))
				{
					return;
				}
				if ((value<=0)||(value>=12))
				{
					 throw new NullReferenceException("Ошибка заполнения номера");
				}
				_number = value;
			}
		}
		public string Letter
		{
			get => _letter;
			set
			{
				if (!string.IsNullOrWhiteSpace(_letter)&&(string.IsNullOrEmpty(value)))
				{
					return;
				}
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new NullReferenceException("Ошибка заполнения литеры");
				}
				_letter = value;
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
				if (value==0)
				{
					throw new NullReferenceException("Ошибка заполнения года");
				}
				_year = value;
			}
		}
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
		public int CurriculumId
		{
			get => _curriculumId;
			set
			{
				if (!(_curriculumId == 0) && (value == 0))
				{
					return;
				}
				if (value == 0)
				{
					throw new NullReferenceException("Ошибка заполнения учебного плана");
				}
				_curriculumId = value;
			}
		}


		private int _number;
		private string _letter=string.Empty;
		private int _year;
		private int _schoolId;
		private int _curriculumId;

	}
}
