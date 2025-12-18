using System.ComponentModel.DataAnnotations;

namespace BoardApi.Dtos;

public record GetPostsByQueryParam(
    [MinLength(2)]
    string? Keyword,
    int Page = 0, 
    int PageSize = 10) {}