namespace E_Commerce.Dtos;

public class PaginatedDto<T>
{
    public int CurrentPage { get; private set; }
    public int PageSize { get; private set; }
    public int TotalItemsCount { get; private set; }
    public IEnumerable<T> Items { get; private set; } = null!;

    public int TotalPages => (int)Math.Ceiling(TotalItemsCount / (double)PageSize);
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;

    private PaginatedDto() { }

    public static PaginatedDto<T> Create(int page, int pageSize, IEnumerable<T> items, int totalItemsCount)
    {
        return new PaginatedDto<T>
        {
            Items = items,
            TotalItemsCount = totalItemsCount,
            CurrentPage = page,
            PageSize = pageSize
        };
    }
}