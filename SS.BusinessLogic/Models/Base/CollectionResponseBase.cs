using SS.BusinessLogic.Models.Filter;
using SS.BusinessLogic.Models.Page;

namespace SS.BusinessLogic.Models.Base;

public class CollectionResponseBase<TDataEntity> : ResponseBase<List<TDataEntity>>
{
    public PageInfoResponse PageInfo { get; set; }

    public List<FilterInfo> Filters { get; set; }
}