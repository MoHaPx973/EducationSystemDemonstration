namespace EducationSystem.Domain.Models
{
	public class Person
	{
		public int Id { get; set; }
		public string Name
		{
			get => _name;
			set
			{
				if (!string.IsNullOrWhiteSpace(_name) && (string.IsNullOrEmpty(value)))
				{
					return;
				}
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new NullReferenceException("Ошибка заполнения имени");
				}
				_name = value;
			}
		}
		public string Surname
		{
			get => _surname;
			set
			{
				if (!string.IsNullOrWhiteSpace(_surname) && (string.IsNullOrEmpty(value)))
				{
					return;
				}
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new NullReferenceException("Ошибка заполнения фамилии");
				}
				_surname = value;
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
		public int PersonTypeId
		{
			get => _personTypeId;
			set
			{
				if (!(_personTypeId==0)&&(value==0))
				{
					return;
				}
				//if ((value>2)||(value<1))
				//{
				//	throw new NullReferenceException("Ошибка заполнения роли");
				//}
				_personTypeId = value;
			}
		}

		private string _name = string.Empty;
		private string _surname = string.Empty;
		private int _schoolId;
		private int _personTypeId;

	}
}
//public int PersonTypeId 
//{
//	get
//	{
//		return (int)this.PersonType;
//	}
//	set 
//	{
//		PersonType = (PersonTypes)value;
//	}
//}
