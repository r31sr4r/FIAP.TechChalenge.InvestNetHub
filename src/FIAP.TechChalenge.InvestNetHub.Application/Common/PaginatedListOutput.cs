namespace FIAP.TechChalenge.InvestNetHub.Application.Common;
public abstract class PaginatedListOutput<TOutputItem>
{
    protected PaginatedListOutput(
        int page, 
        int pageSize, 
        int total, 
        IReadOnlyList<TOutputItem> items)
    {
        Page = page;
        PageSize = pageSize;
        Total = total;
        Items = items;
    }

    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public IReadOnlyList<TOutputItem> Items { get; set; }

}
