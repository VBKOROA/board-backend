using BoardApi.Dtos;
using BoardApi.Enums;

namespace BoardApi.Repositories
{
    public interface IPostRepository
    {
        Task<PagedPostDto> FetchPostsBy(int page, int pageSize, PostSortType postSortType, CommonOrderType orderType, string? keyword);
    }
}