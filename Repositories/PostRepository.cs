using BoardApi.Data;
using BoardApi.Dtos;
using BoardApi.Enums;
using BoardApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BoardApi.Repositories
{
    public class PostRepository(AppDbContext db) : IPostRepository
    {
        private readonly AppDbContext _db = db;

        public async Task<Post?> FindBy(int id) 
        {
            return await _db.Posts.FindAsync(id);
        }

        public async Task Save(Post post)
        {
            _db.Posts.Add(post);
            await _db.SaveChangesAsync();
        }

        public async Task<PagedPostDto> FetchPostsBy(int page, int pageSize, PostSortType postSortType, CommonOrderType orderType, string? keyword)
        {
            IQueryable<Post> query = _db.Posts;

            if (keyword is not null)
            {
                query = query.Where(post => post.Title.Contains(keyword) || post.Content.Contains(keyword));
            }

            var ordered = postSortType switch
            {
                PostSortType.Title => orderType == CommonOrderType.Asc ?
                                    query.OrderBy(post => post.Title)
                                    : query.OrderByDescending(post => post.Title),
                PostSortType.CreatedAt or _ => orderType == CommonOrderType.Asc ?
                                    query.OrderBy(post => post.CreatedAt)
                                    : query.OrderByDescending(post => post.CreatedAt),
            };

            query = ordered.ThenByDescending(post => post.Id);

            var totalPosts = await query.CountAsync();

            var posts = await query
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedPostDto(posts, totalPosts);
        }
    }
}