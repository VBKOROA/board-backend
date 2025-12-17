namespace BoardApi.Dtos;

public record GetPostsByQueryParam(int Page = 0, int PageSize = 10) {}