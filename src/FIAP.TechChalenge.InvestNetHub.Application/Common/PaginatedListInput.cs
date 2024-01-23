using FIAP.TechChalenge.InvestNetHub.Domain.SeedWork.SearchableRepository;

namespace FIAP.TechChalenge.InvestNetHub.Application.Common;
public abstract class PaginatedListInput
{
    public PaginatedListInput(
        int page, 
        int perPage, 
        string search, 
        string sort, 
        SearchOrder dir)
    {
        Page = page;
        PerPage = perPage;
        Search = search;
        Sort = sort;
        Dir = dir;
    }

    public int Page { get; set; } = 1;
    public int PerPage { get; set; } = 10;
    public string Search { get; set; } = string.Empty;
    public string Sort { get; set; }
    public SearchOrder Dir { get; set; } = SearchOrder.Asc;
}
