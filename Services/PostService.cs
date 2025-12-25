using BoardApi.Data;
using BoardApi.Dtos;
using BoardApi.Enums;
using BoardApi.Models;
using BoardApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BoardApi.Services
{
    public class PostService(IPostRepository postRepository) : IPostService
    {
        private readonly IPostRepository _postRepository = postRepository;

        public async Task<PagedPostDto> GetPagedPost(int page, int pageSize, PostSortType postSortType, CommonOrderType orderType, string? keyword)
        {
            return await _postRepository.FetchPostsBy(page, pageSize, postSortType, orderType, keyword);
        }
    }
}