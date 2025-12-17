namespace BoardApi.Common.Dtos;

public record CommonPageResponse<T> (IEnumerable<T> Items, int Page, int PageSize, int TotalItems)
{
    public CommonPageResponse (IEnumerable<T> items, int page, int pageSize) : this(items, page, pageSize, items.Count()) {}
}