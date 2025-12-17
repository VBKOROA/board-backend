using System.ComponentModel.DataAnnotations;

namespace BoardApi.Dtos;

public record EditPostRequest(
    [StringLength(maximumLength: 20, MinimumLength = 1)]
    string? Title,
    [StringLength(maximumLength: 40, MinimumLength = 1)]
    string? Content) : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
         if (Title is null && Content is null)
        {
            yield return new ValidationResult("At least one of Title ore Content must be provided.");
        }
    }
}