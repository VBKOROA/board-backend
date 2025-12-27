using BoardApi.Dtos;
using BoardApi.Enums;
using BoardApi.Models;

namespace BoardApi.Repositories
{
    public interface IPostRepository
    {
        Task<PagedPostDto> FetchPostsBy(int page, int pageSize, PostSortType postSortType, CommonOrderType orderType, string? keyword);
        Task Add(Post post);
        Task<Post?> FindBy(int id);
    }
}