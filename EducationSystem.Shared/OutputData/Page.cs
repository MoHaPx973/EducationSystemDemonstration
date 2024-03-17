namespace EducationSystem.Shared.OutputData
{
	public class Page<T>
	{
		public int Start { get; set; }
		public int Count { get; set; }
		public IEnumerable<T>? Items { get; set; }
		public Page(int start, int count, IEnumerable<T>? items)
		{
			this.Start = start;
			this.Count = count;

			if (items != null)
			{
				this.Items = items.Skip(start * count).Take(count);
				if (this.Items.Count()
					== 0)
					this.Items = null;
			}
			else
			{
				this.Items = null;
			}

		}
		//Page<IQueryable<School>> pages = new Page<IQueryable<School>>();
		//IQueryable<School> schools = _genericRepository.GetAllQueryable();
	}
}
