namespace EducationSystem.Domain.Models
{
    public class School
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
					throw new NullReferenceException("Ошибка заполнения названия");
				}
				_name = value;
			}
		}
		public string City
		{
			get => _city;
			set
			{
				if (!string.IsNullOrWhiteSpace(_city) && (string.IsNullOrEmpty(value)))
				{
					return;
				}
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new NullReferenceException("Ошибка заполнения города");
				}
				_city = value;
			}
		}

		private string _name = string.Empty;
		private string _city = string.Empty;
	}
}
