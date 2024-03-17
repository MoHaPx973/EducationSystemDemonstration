using System.Xml.Linq;

namespace EducationSystem.Domain.Models
{
	public class SchoolItem
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
		private string _name=string.Empty;
	}
}
