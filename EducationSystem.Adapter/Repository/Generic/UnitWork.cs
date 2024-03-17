using EducationSystem.App.Storage.GenericInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.Adapter.Repository.Generic
{
	public class UnitWork:IUnitWork
	{
		private readonly EducationDbContext _context;
		public UnitWork(EducationDbContext context)
		{
			this._context = context;
		}
		public Task Commit()
		{
			return _context.SaveChangesAsync();
		}
		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
