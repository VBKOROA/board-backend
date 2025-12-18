using System.ComponentModel.DataAnnotations;
using BoardApi.Enums;

namespace BoardApi.Dtos;

public record GetPostsByQueryParam(
    [MinLength(2)]
    string? Keyword,
    int Page = 0, 
    int PageSize = 10,
    PostSortType Sort = PostSortType.CreatedAt,
    CommonOrderType Order = CommonOrderType.Desc) {}