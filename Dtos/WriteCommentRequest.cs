using System.ComponentModel.DataAnnotations;
using BoardApi.Models;

namespace BoardApi.Dtos
{
    public record WriteCommentRequest(
        [Required]
        [MinLength(Comment.ContentsMinLength)]
        [MaxLength(Comment.ContentsMaxLength)]
        string Contents
    ){}
}