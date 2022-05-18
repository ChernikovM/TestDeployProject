using SS.DataAccess.Entities.Base;

namespace SS.DataAccess.Entities;

public class StickerPack : EntityBase
{
    public uint? Count { get; set; }

    public string SourceLink { get; set; }

    public string TelegramLink { get; set; }

    public List<Label> Labels { get; set; }
}