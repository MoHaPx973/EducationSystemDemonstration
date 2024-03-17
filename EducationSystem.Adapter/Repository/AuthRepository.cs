using EducationSystem.Adapter.Repository.Generic;
using EducationSystem.App.Storage.AuthInterface;
using EducationSystem.Domain.AuthModels;
using Microsoft.EntityFrameworkCore;


namespace EducationSystem.Adapter.Repository
{
	public class AuthRepository:GenericRepository<User>,IAuthRepository
	{
		public AuthRepository(EducationDbContext context) : base(context) { }
		
		//public  List<User> GetAllAsync()
		//{
		//	return  _context.Set<TEntity>().ToList();
		//}

		//public async Task<TEntity?> GetByIdAsync(int id)
		//{
		//	return await _context.Set<TEntity>().FindAsync(id);
		//}

		//public IQueryable<TEntity> GetAllQueryable()
		//{
		//	return _context.Set<TEntity>();
		//}

		//public IEnumerable<TEntity> GetAllEnumerable()
		//{
		//	return _context.Set<TEntity>();
		//}

		//public void Insert(TEntity entity)
		//{
		//	_context.Set<TEntity>().Add(entity);
		//}

		//public void Update(TEntity entity)
		//{
		//	_context.Update(entity);
		//}
		//public void Delete(TEntity entity)
		//{
		//	_context.Remove(entity);
		//}
		public async Task<User?> GetByName(string login)
		{
			return await _context.Users.FirstOrDefaultAsync(x => x.Login == login);
		}
	}
}
