using Ardalis.Specification;
 
namespace rivne.booking.Core.Interfaces;
public interface IRepository<TEntity> where TEntity : class, IEntity
{
	Task Save();
	Task<IEnumerable<TEntity>> GetAll();
	Task<TEntity?> GetById(object id);
	Task Insert(TEntity entity);
	Task Update(TEntity entityToUpdate);
	Task Delete(TEntity entity);
	Task Delete(object id);
	Task<TEntity?> GetItemBySpec(ISpecification<TEntity> specification);
	Task<IEnumerable<TEntity>> GetListBySpec(ISpecification<TEntity> specification);
}
