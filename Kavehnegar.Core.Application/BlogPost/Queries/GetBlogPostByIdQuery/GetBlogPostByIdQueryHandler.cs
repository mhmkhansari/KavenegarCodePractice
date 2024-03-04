using Kavehnegar.Shared.Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Kavehnegar.External.Infrastructure;
using Newtonsoft;
using Newtonsoft.Json;
namespace Kavehnegar.Core.Application.BlogPost.Queries.GetBlogPostByIdQuery
{
    public sealed class GetBlogPostByIdQueryHandler : IQueryHandler<GetBlogPostByIdQuery, BlogPostResponse>
    {
        private readonly KavehnegarDbContext _dbContext;
        private readonly IDistributedCache _distributedCache;

        public GetBlogPostByIdQueryHandler(KavehnegarDbContext dbContext, IDistributedCache distributedCache) 
        {
            _dbContext = dbContext;
            _distributedCache = distributedCache;
        }
        public async Task<BlogPostResponse> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
        {
            string Key = $"member-{request.BlogPostId}";
            string? cachedBlogPost = await _distributedCache.GetStringAsync(Key, cancellationToken);
            BlogPostResponse? blogPost;
            if (cachedBlogPost == null)
            {
                blogPost = _dbContext.BlogPosts.AsNoTracking().Where(x => x.Id == request.BlogPostId)
                    .Select(s => new BlogPostResponse(
                        s.Id,
                        s.Title,
                        s.Description,
                        s.authorId,
                        s.Author.username

                    )).FirstOrDefault();
                if (blogPost == null)
                {
                    return blogPost;
                }
                await _distributedCache.SetStringAsync(Key, JsonConvert.SerializeObject(blogPost), cancellationToken);
                return blogPost;
            }
            blogPost = JsonConvert.DeserializeObject<BlogPostResponse>(cachedBlogPost,
                new JsonSerializerSettings
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                });
            return blogPost;
        }
    }
}
