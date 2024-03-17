namespace EducationSystem.Domain.Relationships
{
	public class StudentInClass
	{
        public int StudentId
        {
            get => _studentId;
            set
            {
                if (!(_studentId == 0) && value == 0)
                {
                    return;
                }
                if (value == 0)
                {
                    throw new NullReferenceException("Ошибка заполнения ученика");
                }
				_studentId = value;
            }
        }
        public int SchoolClassId
		{
            get => _schoolClassId;
            set
            {
                if (!(_schoolClassId == 0) && value == 0)
                {
                    return;
                }
                if (value == 0)
                {
                    throw new NullReferenceException("Ошибка заполнения класса");
                }
				_schoolClassId = value;
            }
        }
        private int _studentId;
        private int _schoolClassId;
    }
}
