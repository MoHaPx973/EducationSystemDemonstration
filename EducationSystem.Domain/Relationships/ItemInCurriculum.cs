using EducationSystem.Domain.Models;

namespace EducationSystem.Domain.Relationships
{
    public class ItemInCurriculum
    {
        public int ItemId
        {
            get => _itemId;
            set
            {
                if (!(_itemId == 0) && value == 0)
                {
                    return;
                }
                if (value == 0)
                {
                    throw new NullReferenceException("Ошибка заполнения предмета");
                }
                _itemId = value;
            }
        }
        public int CurriculumId
        {
            get => _curriculumId;
            set
            {
                if (!(_curriculumId == 0) && value == 0)
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
		private int _itemId;
        private int _curriculumId;
    }
}
