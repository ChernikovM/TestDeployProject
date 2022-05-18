using SS.DataAccess.Entities;
using SS.DataAccess.Repositories.EF.Base;
using SS.DataAccess.Repositories.EF.Interfaces;

namespace SS.DataAccess.Repositories.EF;

public class StickerPackRepository : EFRepository<StickerPack>, IStickerPackRepository
{
    public StickerPackRepository(AppContext context) : base(context)
    {
    }
}