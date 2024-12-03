namespace PetFamily.Core.Models;
public class PagedList<T>
{
    public IReadOnlyList<T> Items { get; set; }
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public bool NextPage => Page * PageSize < TotalCount;
    public bool PreviousPage => Page > 1;

}
