using SS.BusinessLogic.Models.Base;
using SS.BusinessLogic.Models.Page;

namespace SS.BusinessLogic.Providers;

public static class CollectionDataAccessProvider
{
    // Async method can not returns out parameters
    // public static async Task<IQueryable<TEntity>> FilterData<TEntity>(
    //     IRepository<TEntity> repository, CollectionRequestBase request, bool includeRemoved = false)
    //     where TEntity : EntityBase
    // {
    //     IQueryable<TEntity> entities;
    //
    //     Func<TEntity, bool> expression = FilteringProvider.BuildFilter<TEntity>(
    //         request.Filters,
    //         out List<FilterInfo> validFilters);
    //
    //     if (expression is not null)
    //     {
    //         entities = (await repository.FindAsync(expression, includeRemoved)).AsQueryable();
    //     }
    //     else
    //     {
    //         entities = await repository.GetAllAsync();
    //     }
    //
    //     return entities;
    // }

    public static List<TEntity> PaginateData<TEntity>(
        List<TEntity> entities, CollectionRequestBase request, out PageInfoResponse pageInfo)
    {
        List<TEntity> result;
        pageInfo = new PageInfoResponse();

        if (request.Page is not null &&
            request.PageSize is not null &&
            request.Page is > 0 and < uint.MaxValue / 200 &&
            request.PageSize is > 0 and < 200)
        {
            result = entities
                .Skip((int) ((request.Page - 1) * request.PageSize))
                .Take((int) request.PageSize)
                .ToList();

            pageInfo.CurrentPage = request.Page;
            pageInfo.FullCollectionSize = entities.Count;
            pageInfo.PageSize = request.PageSize;
        }
        else
        {
            result = entities
                .Take(20)
                .ToList();

            pageInfo.CurrentPage = 1;
            pageInfo.FullCollectionSize = entities.Count;
            pageInfo.PageSize = 20;
        }

        return result;
    }
}