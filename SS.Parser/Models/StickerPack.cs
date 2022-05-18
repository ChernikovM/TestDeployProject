namespace SS.Parser.Models;

public class StickerPack
{
    public string Name { get; set; }

    public uint? Count { get; set; }

    public List<string> Labels { get; set; }

    public string SourceLink { get; set; }

    public string TelegramLink { get; set; }
}