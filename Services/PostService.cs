using BoardApi.Data;
using BoardApi.Dtos;
using BoardApi.Enums;
using BoardApi.Exceptions;
using BoardApi.Models;
using BoardApi.Repositories;

namespace BoardApi.Services
{
    public class PostService(IPostRepository postRepository, IUnitOfWork uow) : IPostService
    {
        private readonly IPostRepository _postRepository = postRepository;
        private readonly IUnitOfWork _uow = uow;

        public async Task EditPostBy(int id, string? title, string? content)
        {
            Post post = await FetchPostBy(id);

            if (title is not null)
            {
                post.Title = title;
            }

            if (content is not null)
            {
                post.Content = content;
            }

            await _uow.SaveChnages();
        }

        public async Task<PostDto> GetPostBy(int id)
        {
            var post = await FetchPostBy(id);

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
            await _uow.SaveChnages();
            
            return new PostDto(post);
        }

        public async Task<PagedPostDto> GetPagedPost(int page, int pageSize, PostSortType postSortType, CommonOrderType orderType, string? keyword)
        {
            return await _postRepository.FetchPostsBy(page, pageSize, postSortType, orderType, keyword);
        }

        public async Task DeletePostBy(int id)
        {
            var post = await FetchPostBy(id);
            _postRepository.Delete(post);
            await _uow.SaveChnages();
        }

        public async Task<CommentDto> WriteCommentTo(int postId, string content)
        {
            var post = await FetchPostBy(postId);
            var comment = post.AddNewComment(content);
            await _uow.SaveChnages();
            return new CommentDto(comment);
        }

        private async Task<Post> FetchPostBy(int id)
        {
            return await _postRepository.FindBy(id) ?? throw new BusinessException(CommonErrorCode.PostNotFound);
        }
    }
}