using BoardApi.Dtos;
using BoardApi.Enums;
using BoardApi.Models;
using BoardApi.Repositories;

namespace BoardApi.Services
{
    public class PostService(IPostRepository postRepository) : IPostService
    {
        private readonly IPostRepository _postRepository = postRepository;

        public async Task<PostDto> CreatePost(string title, string? content)
        {
            var post = new Post
            {
                Title = title,
                Content = content ?? ""
            };

            
            await _postRepository.Save(post);

            return new PostDto(post);
        }

        public async Task<PagedPostDto> GetPagedPost(int page, int pageSize, PostSortType postSortType, CommonOrderType orderType, string? keyword)
        {
            return await _postRepository.FetchPostsBy(page, pageSize, postSortType, orderType, keyword);
        }
    }
}