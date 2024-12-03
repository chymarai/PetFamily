namespace PetFamily.Core.Models;
public class OnePageList<T>
{
    public IReadOnlyList<T> Items { get; set; }
    public int TotalCount { get; set; }

}
