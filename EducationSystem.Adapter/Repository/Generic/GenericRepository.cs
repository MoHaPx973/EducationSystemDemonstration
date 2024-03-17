using EducationSystem.App.Storage.GenericInterface;
using Microsoft.EntityFrameworkCore;

namespace EducationSystem.Adapter.Repository.Generic
{
	public class GenericRepository<TEntity>:IGenericRepository<TEntity>
		where TEntity : class
	{
		protected EducationDbContext _context;
		public GenericRepository(EducationDbContext context)
		{ 
			this._context = context; 
		}

		public  List<TEntity> GetAllAsync()
		{
			return  _context.Set<TEntity>().ToList();
		}

		public async Task<TEntity?> GetByIdAsync(int id)
		{
			return await _context.Set<TEntity>().FindAsync(id);
		}

		public IQueryable<TEntity> GetAllQueryable()
		{
			return _context.Set<TEntity>();
		}

		public IEnumerable<TEntity> GetAllEnumerable()
		{
			return _context.Set<TEntity>();
		}

		public void Insert(TEntity entity)
		{
			_context.Set<TEntity>().Add(entity);
		}

		public void Update(TEntity entity)
		{
			_context.Update(entity);
		}
		public void Delete(TEntity entity)
		{
			_context.Remove(entity);
		}
		public async Task<TEntity?> CombinedKey(int firstId, int secondId)
		{
			return await _context.Set<TEntity>().FindAsync(firstId,secondId);
		}
	}
}

