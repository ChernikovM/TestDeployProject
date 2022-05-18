using SS.Parser.Models;

namespace SS.Parser.Base;

public interface IParserBase
{
    Task<List<StickerPack>> ParseAsync();
}