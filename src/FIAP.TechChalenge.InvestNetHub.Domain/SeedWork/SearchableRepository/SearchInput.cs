namespace FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;
public class SearchInput
{
    public SearchInput(
        int page, 
        int perPage, 
        string orderBy, 
        SearchOrder order, 
        string search)
    {
        Page = page;
        PerPage = perPage;
        OrderBy = orderBy;
        Order = order;
        Search = search;
    }

    public int Page { get; set; }
    public int PerPage { get; set; }
    public string OrderBy { get; set; }
    public SearchOrder Order { get; set; }
    public string Search { get; set; }
}
