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

	public async Task<IEnumerable<TEntity>> GetAll()
	{
		return await _dbSet.ToListAsync();
	}

	public async Task Insert(TEntity entity)
	{
		await _dbSet.AddAsync(entity);
	}

	public async Task Save()
	{
		await _context.SaveChangesAsync();
	}

	public async Task Update(TEntity ententityToUpdate)
	{
		await Task.Run
			(
			() =>
			{
				_dbSet.Attach(ententityToUpdate);
				_context.Entry(ententityToUpdate).State = EntityState.Modified;
			});
	}
	 
	public async Task<TEntity?> GetById(object id)
	{
		return await _dbSet.FindAsync(id);
	}
	public async Task Delete(object id)
	{
		TEntity? entityToDelete = await _dbSet.FindAsync(id);
		if (entityToDelete != null)
		{
			await Delete(entityToDelete);
		}
	}

	public async Task Delete(TEntity entityToDelete)
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

	public async Task<IEnumerable<TEntity>> GetListBySpec(ISpecification<TEntity> specification)
	{
		return await ApplySpecification(specification).ToListAsync();
	}

	
}