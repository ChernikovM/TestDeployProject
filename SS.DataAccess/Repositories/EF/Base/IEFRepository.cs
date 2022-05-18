using SS.DataAccess.Entities.Base;
using SS.DataAccess.Repositories.Base;

namespace SS.DataAccess.Repositories.EF.Base;

// ReSharper disable once InconsistentNaming
public interface IEFRepository<TEntity> : IRepository<TEntity>
    where TEntity : EntityBase
{
}