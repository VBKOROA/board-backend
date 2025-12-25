using BoardApi.Models;

namespace BoardApi.Dtos
{
    public record PagedPostDto(IReadOnlyList<Post> Posts, int TotalPosts);
}