using BoardApi.Dtos;
using BoardApi.Enums;

namespace BoardApi.Services
{
    public interface IPostService
    {
        Task<PagedPostDto> GetPagedPost(int page, int pageSize, PostSortType postSortType, CommonOrderType orderType, string? keyword);
        Task<PostDto> CreatePost(string title, string? content);
        Task<PostDto> GetPostBy(int id);
        Task EditPostBy(int id, string? title, string? content);
        Task DeletePostBy(int id);
        Task<CommentDto> WriteCommentTo(int postId, string content);
    }
}