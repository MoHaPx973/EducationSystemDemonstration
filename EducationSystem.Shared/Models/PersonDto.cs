namespace EducationSystem.Shared.Models
{
	public class PersonDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Surname { get; set; } = string.Empty;
		public int SchoolId{ get; set; }
		public int PersonTypeId
		{
			get => _personTypeId;
			set
			{
				if (!(_personTypeId == 0) && (value == 0))
				{
					return;
				}
				//if (value == 0)
				//{
				//	throw new NullReferenceException("Ошибка заполнения роли");
				//}
				_personTypeId = value;
			}
		}
		public string? PersonType
		{
			get
			{
				switch (_personTypeId)
				{
					case 1: return "student";
					case 2: return "teacher";
					default: return null;
				}

			}
		}
		private int _personTypeId;
	}
}
