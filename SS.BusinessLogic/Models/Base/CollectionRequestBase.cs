using SS.BusinessLogic.Models.Filter;

namespace SS.BusinessLogic.Models.Base;

public class CollectionRequestBase
{
    public List<FilterInfo> Filters { get; set; }

    public List<uint> LabelIds { get; set; }

    public uint? Page { get; set; }

    public uint? PageSize { get; set; }
}