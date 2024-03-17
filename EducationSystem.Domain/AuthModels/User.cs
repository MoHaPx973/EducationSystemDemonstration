namespace EducationSystem.Domain.AuthModels
{
	public class User
	{
		public int Id { get; set; }
		public string Login
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
		public string Password
		{
			get => _password;
			set
			{
				if (!string.IsNullOrWhiteSpace(_password) && (string.IsNullOrEmpty(value)))
				{
					return;
				}
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new NullReferenceException("Ошибка заполнения имени");
				}
				_password = value;
			}
		}
		public string Role
		{
			get => _role;
			set
			{
				if (!string.IsNullOrWhiteSpace(_role) && (string.IsNullOrEmpty(value)))
				{
					return;
				}
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new NullReferenceException("Ошибка заполнения имени");
				}
				_role = value;
			}
		}
        public int PersonId 
		{
			get => _personId;
			set
			{
				if ((_personId!=0) && (value==0))
				{
					return;
				}
				if (value==0)
				{
					throw new NullReferenceException("Ошибка заполнения имени");
				}
				_personId = value;
			}
		}

        private string _name;
		private string _password;
		private string _role;
		private int _personId;
	}
}
