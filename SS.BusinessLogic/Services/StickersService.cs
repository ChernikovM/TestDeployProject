using SS.BusinessLogic.Models.Base;
using SS.BusinessLogic.Models.Filter;
using SS.BusinessLogic.Models.Labels;
using SS.BusinessLogic.Models.Page;
using SS.BusinessLogic.Models.StickerPacks;
using SS.BusinessLogic.Providers;
using SS.BusinessLogic.Services.Interfaces;
using SS.DataAccess.Repositories.EF.Interfaces;

namespace SS.BusinessLogic.Services;

public class StickersService : IStickersService
{
    private readonly IStickerPackRepository _repository;

    public StickersService(IStickerPackRepository repository)
    {
        this._repository = repository;
    }

    public async Task<CollectionResponseBase<StickerPack>> GetStickerPacks(CollectionRequestBase request)
    {
        CollectionResponseBase<StickerPack> response = new CollectionResponseBase<StickerPack>();
        List<DataAccess.Entities.StickerPack> entities = (await this._repository.GetAllAsync()).ToList();

        Func<DataAccess.Entities.StickerPack, bool> expression =
            FilteringProvider.BuildFilter<DataAccess.Entities.StickerPack>(request?.Filters, out List<FilterInfo> validFilters);

        if (request?.LabelIds?.Any() is true)
        {
            entities = entities.Where(entity =>
                    entity.Labels.Exists(label => request.LabelIds.Contains((uint) label.Id)))
                .ToList();
        }

        PageInfoResponse pageInfo = null;

        if (entities.Any())
        {
            if (expression is not null)
            {
                entities = entities.Where(expression).ToList();
            }

            entities = CollectionDataAccessProvider.PaginateData(entities, request, out pageInfo);
        }

        response.Filters = validFilters;
        response.PageInfo = pageInfo;
        response.Data = entities.ToList().Select(this.MapStickerPack).ToList();

        return response;
    }

    private StickerPack MapStickerPack(DataAccess.Entities.StickerPack entryEntity)
    {
        StickerPack pack = new StickerPack
        {
            Id = entryEntity.Id,
            Count = entryEntity.Count,
            Labels = entryEntity.Labels.Select(x => new Label(x)).ToList(),
            Name = entryEntity.Name,
            TelegramLink = entryEntity.TelegramLink,
            IsRemoved = entryEntity.IsRemoved
        };

        return pack;
    }
}