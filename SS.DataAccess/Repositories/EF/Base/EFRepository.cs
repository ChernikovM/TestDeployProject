using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SS.DataAccess.Entities;
using SS.DataAccess.Entities.Base;

namespace SS.DataAccess.Repositories.EF.Base;

// ReSharper disable once InconsistentNaming
public class EFRepository<TEntity> : IEFRepository<TEntity>
    where TEntity : EntityBase
{
    private readonly AppContext _context;
    private readonly IQueryable<TEntity> _dbSet;

    private IQueryable<TEntity> NotRemovedItems => this._dbSet.Where(entity => entity.IsRemoved == false);

    private IQueryable GetDbSet(AppContext context)
    {
        IQueryable dbSet = context.Set<TEntity>();

        if (typeof(TEntity) == typeof(StickerPack))
        {
            dbSet = context.Set<StickerPack>().Include(entity => entity.Labels);
        }
        else if (typeof(TEntity) == typeof(Label))
        {
            dbSet = context.Set<Label>().Include(entity => entity.StickerPacks);
        }

        return dbSet;
    }

    public EFRepository(AppContext context)
    {
        this._context = context;
        this._dbSet = this.GetDbSet(this._context) as IQueryable<TEntity>;
    }

    public async Task<EntityEntry<TEntity>> AddAsync(TEntity item)
    {
        return await this._context
            .AddAsync(this.PrepareItemForAdding(item));
    }

    public async Task AddRangeAsync(List<TEntity> items)
    {
        await this._context
            .AddRangeAsync(items.Select(this.PrepareItemForAdding));
    }

    public async Task<TEntity> FindByIdAsync(long id, bool includeRemoved = false)
    {
        IQueryable<TEntity> set = includeRemoved
            ? this._dbSet
            : this.NotRemovedItems;

        return await set
            .FirstOrDefaultAsync(entity => entity.Id == id);
    }

    public async Task<IQueryable<TEntity>> GetAllAsync(bool includeRemoved = false)
    {
        IQueryable<TEntity> set = includeRemoved
            ? this._dbSet
            : this.NotRemovedItems;

        return await Task.FromResult(set.AsQueryable());
    }

    public async Task<IEnumerable<TEntity>> FindAsync(Func<TEntity, bool> predicate, bool includeRemoved = false)
    {
        IQueryable<TEntity> set = includeRemoved
            ? this._dbSet
            : this.NotRemovedItems;

        return (await set
                .ToListAsync())
            .Where(predicate);
    }

    public async Task<bool> ExistAsync(Func<TEntity, bool> predicate, bool includeRemoved = false)
    {
        return (await this.FindAsync(predicate, includeRemoved))
            ?.FirstOrDefault() is not null;
    }

    public async Task<EntityEntry<TEntity>> RemoveAsync(TEntity item)
    {
        item.IsRemoved = true;

        return await this.UpdateAsync(item);
    }

    public async Task<EntityEntry<TEntity>> UpdateAsync(TEntity item)
    {
        item.RecModified = DateTime.UtcNow;

        return await Task.FromResult(this._context.Update(item));
    }

    public async Task<int> SaveChanges()
    {
        return await this._context.SaveChangesAsync();
    }

    private TEntity PrepareItemForAdding(TEntity item)
    {
        item.RecCreated = DateTime.UtcNow;
        item.RecModified = item.RecCreated;
        item.IsRemoved = false;

        return item;
    }
}