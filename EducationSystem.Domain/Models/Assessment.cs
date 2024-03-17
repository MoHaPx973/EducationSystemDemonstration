using System.Data;

namespace EducationSystem.Domain.Models
{
	public class Assessment
	{
		public int Id { get; set; }
		public int StudentId
		{
			get => _studentId;
			set
			{
				if (!(_studentId == 0) && (value == 0))
				{
					return;
				}
				if (value == 0)
				{
					throw new NullReferenceException("Ошибка заполнения информации об ученике");
				}
				_studentId = value;
			}
		}
		public int TeacherId 
		{
			get => _teacherId;
			set
			{
				if (!(_teacherId == 0) && (value == 0))
				{
					return;
				}
				if (value == 0)
				{
					throw new NullReferenceException("Ошибка заполнения информации об учителе");
				}
				_teacherId = value;
			}
		}
        public int SchoolClassId 
		{
			get => classId;
			set
			{
				if (!(classId == 0) && (value == 0))
				{
					return;
				}
				if (value == 0)
				{
					throw new NullReferenceException("Ошибка заполнения информации об учителе");
				}
				classId = value;
			}
		}
        public int ItemId 
		{
			get => _itemId;
			set
			{
				if (!(_itemId == 0) && (value == 0))
				{
					return;
				}
				if (value == 0)
				{
					throw new NullReferenceException("Ошибка заполнения информации об предмете");
				}
				_itemId = value;
			}
		}
		public DateTime Date 
		{
			get => _date;
			set
			{
				if (!(_date == checkDateTime) && (value == checkDateTime))
				{
					return;
				}
				if (value == checkDateTime)
				{
					throw new NullReferenceException("Ошибка заполнения информарации об дате");
				}
				_date = value;
			} 
		}
		public int Point 
		{
			get => _point;
			set
			{
				if (!(_point == 0) && (value == 0))
				{
					return;
				}
				if ((value == 0)|| (value > 5)|| (value < 1))
				{
					throw new NullReferenceException("Ошибка заполнения информации об оценке");
				}
				_point = value;
			}
		}

		private int _studentId;
		private int _teacherId;
		private int classId;
		private int _itemId;
		private DateTime _date;
		private DateTime checkDateTime = new DateTime();
		private int _point;

		public Assessment() { }
		public Assessment(int studentId, int teacherId, int schoolClassId, int itemId, DateTime date, int point)
		{
			StudentId = studentId;
			TeacherId = teacherId;
			SchoolClassId = schoolClassId;
			ItemId = itemId;
			Date = date;
			Point = point;
		}
	}
}
