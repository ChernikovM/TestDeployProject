using SS.BusinessLogic.Models.Base;
using SS.BusinessLogic.Models.StickerPacks;

namespace SS.BusinessLogic.Services.Interfaces;

public interface IStickersService
{
    Task<CollectionResponseBase<StickerPack>> GetStickerPacks(CollectionRequestBase request);
}