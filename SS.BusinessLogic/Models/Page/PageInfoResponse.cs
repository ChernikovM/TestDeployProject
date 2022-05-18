namespace SS.BusinessLogic.Models.Page;

public class PageInfoResponse
{
    public uint? CurrentPage { get; set; }

    public uint? PageSize { get; set; }

    public uint? TotalPages { get; set; }

    public bool? HasNextPage => this.CurrentPage < this.TotalPages;

    public bool? HasPreviousPage => this.CurrentPage > 1;
}