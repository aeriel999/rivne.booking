using Ardalis.Specification;
 
namespace rivne.booking.Core.Interfaces;
public interface IRepository<TEntity> where TEntity : class, IEntity 
{
	Task SaveAsync();
	Task<IEnumerable<TEntity>> GetAllAsync();
	Task<TEntity?> GetByIdAsync(object id);
	Task InsertAsync(TEntity entity);
	Task UpdateAsync(TEntity entityToUpdate);
	Task DeleteAsync(TEntity entity);
	Task DeleteAsync(object id);
	Task<TEntity?> GetItemBySpec(ISpecification<TEntity> specification);
	Task<IEnumerable<TEntity>> GetListBySpecAsync(ISpecification<TEntity> specification);
}
