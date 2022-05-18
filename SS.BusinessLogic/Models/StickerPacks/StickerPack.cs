using SS.BusinessLogic.Models.Labels;

namespace SS.BusinessLogic.Models.StickerPacks;

public class StickerPack
{
    public long? Id { get; set; }

    public string Name { get; set; }

    public uint? Count { get; set; }

    public string TelegramLink { get; set; }

    public List<Label> Labels { get; set; }

    public bool? IsRemoved { get; set; }
}