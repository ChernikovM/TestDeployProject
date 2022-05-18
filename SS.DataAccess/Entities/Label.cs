using SS.DataAccess.Entities.Base;

namespace SS.DataAccess.Entities;

public class Label : EntityBase
{
    public List<StickerPack> StickerPacks { get; set; }
}