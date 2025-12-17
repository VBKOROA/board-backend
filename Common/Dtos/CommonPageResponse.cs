namespace BoardApi.Common.Dtos;

public record CommonPageResponse<T> (IReadOnlyList<T> Items, int Page, int PageSize, int TotalItems) {}