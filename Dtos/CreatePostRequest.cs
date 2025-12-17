using System.ComponentModel.DataAnnotations;

namespace BoardApi.Dtos;

public record CreatePostRequest(
    [Required]
    [StringLength(maximumLength: 20, MinimumLength = 1)]
    string Title,
    [StringLength(maximumLength: 40, MinimumLength = 1)]
    string? Content)
{ }