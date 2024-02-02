using Ardalis.Specification.EntityFrameworkCore;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using rivne.booking.Core.Interfaces;
using rivne.booking.Infrastructure.Context;


namespace rivne.booking.Infrastructure.Repository;
internal class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity 
{
	internal ApiDbContext _context;
	internal DbSet<TEntity> _dbSet;
	 
	public Repository(ApiDbContext context)
	{
		this._context = context;
		this._dbSet = context.Set<TEntity>();
	}

	public async Task<IEnumerable<TEntity>> GetAllAsync()
	{
		return await _dbSet.ToListAsync();
	}

	public async Task InsertAsync(TEntity entity)
	{
		await _dbSet.AddAsync(entity);
	}

	public async Task SaveAsync()
	{
		await _context.SaveChangesAsync();
	}

	public async Task UpdateAsync(TEntity ententityToUpdate)
	{
		await Task.Run
			(
			() =>
			{
				_dbSet.Attach(ententityToUpdate);
				_context.Entry(ententityToUpdate).State = EntityState.Modified;
			});
	}
	 
	public async Task<TEntity?> GetByIdAsync(object id)
	{
		return await _dbSet.FindAsync(id);
	}
	public async Task DeleteAsync(object id)
	{
		TEntity? entityToDelete = await _dbSet.FindAsync(id);
		if (entityToDelete != null)
		{
			await DeleteAsync(entityToDelete);
		}
	}

	public async Task DeleteAsync(TEntity entityToDelete)
	{
		await Task.Run(
			() =>
			{
				if (_context.Entry(entityToDelete).State == EntityState.Detached)
				{
					_dbSet.Attach(entityToDelete);
				}
				_dbSet.Remove(entityToDelete);
			});
	}

	public async Task<TEntity?> GetItemBySpec(ISpecification<TEntity> specification)
	{
		return await ApplySpecification(specification).FirstOrDefaultAsync();
	}

	private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
	{
		var evaluator = new SpecificationEvaluator();
		return evaluator.GetQuery(_dbSet, specification);
	}

	public async Task<IEnumerable<TEntity>> GetListBySpecAsync(ISpecification<TEntity> specification)
	{
		return await ApplySpecification(specification).ToListAsync();
	}
}