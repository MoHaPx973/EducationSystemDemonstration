using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.App.Storage.GenericInterface
{
	public interface IGenericRepository<TEntity>
	{
		List<TEntity> GetAllAsync();

		Task<TEntity?> GetByIdAsync(int id);

		IQueryable<TEntity> GetAllQueryable();
		IEnumerable<TEntity> GetAllEnumerable();

		void Insert(TEntity entity);

		void Update(TEntity entity);

		void Delete(TEntity entity);

		Task<TEntity?> CombinedKey(int firstId, int secondId);
	}
}
