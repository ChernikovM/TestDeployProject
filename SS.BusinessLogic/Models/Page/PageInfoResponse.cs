namespace SS.BusinessLogic.Models.Page;

public class PageInfoResponse
{
    public uint? CurrentPage { get; set; }

    public uint? PageSize { get; set; }

    public int? FullCollectionSize { get; set; }
}