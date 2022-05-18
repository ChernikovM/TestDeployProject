using Microsoft.EntityFrameworkCore.ChangeTracking;
using SS.DataAccess.Entities.Base;

namespace SS.DataAccess.Repositories.Base;

public interface IRepository<TEntity>
    where TEntity : EntityBase
{
    Task<EntityEntry<TEntity>> AddAsync(TEntity item);

    Task AddRangeAsync(List<TEntity> items);

    Task<TEntity> FindByIdAsync(long id, bool includeRemoved = false);

    Task<IQueryable<TEntity>> GetAllAsync(bool includeRemoved = false);

    Task<bool> ExistAsync(Func<TEntity, bool> predicate, bool includeRemoved = false);

    Task<IEnumerable<TEntity>> FindAsync(Func<TEntity, bool> predicate, bool includeRemoved = false);

    Task<EntityEntry<TEntity>> RemoveAsync(TEntity item);

    Task<EntityEntry<TEntity>> UpdateAsync(TEntity item);

    Task<int> SaveChanges();
}