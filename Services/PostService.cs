using BoardApi.Dtos;
using BoardApi.Enums;
using BoardApi.Models;
using BoardApi.Repositories;

namespace BoardApi.Services
{
    public class PostService(IPostRepository postRepository) : IPostService
    {
        private readonly IPostRepository _postRepository = postRepository;

        public async Task EditPostBy(int id, string? title, string? content)
        {
            var post = await _postRepository.FindBy(id) ?? throw new Exception();
            
            if (title is not null)
            {
                post.Title = title;
            }

            if (content is not null)
            {
                post.Content = content;
            }

            await _postRepository.SaveChanges();
        }

        public async Task<PostDto?> GetPostBy(int id)
        {
            var post = await _postRepository.FindBy(id);

            if (post is null)
            {
                return null;
            }

            return new PostDto(post);
        }

        public async Task<PostDto> CreatePost(string title, string? content)
        {
            var post = new Post
            {
                Title = title,
                Content = content ?? ""
            };


            await _postRepository.Add(post);

            return new PostDto(post);
        }

        public async Task<PagedPostDto> GetPagedPost(int page, int pageSize, PostSortType postSortType, CommonOrderType orderType, string? keyword)
        {
            return await _postRepository.FetchPostsBy(page, pageSize, postSortType, orderType, keyword);
        }
    }
}