using Kavehnegar.Core.Domain.BlogPost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.External.Infrastructure.Repositories
{
    public sealed class BlogPostRepository : IBlogPostRepository
    {
        private readonly KavehnegarDbContext _dbContext;

        public BlogPostRepository(KavehnegarDbContext dbContext) =>
            _dbContext = dbContext;
        public void Delete(BlogPost blogPost)
        {
            _dbContext.BlogPosts.Remove(blogPost);
        }

        public void Insert(BlogPost blogPost)
        {
            _dbContext.BlogPosts.AddAsync(blogPost);
        }

        public async Task<BlogPost?> Load(BlogPostId id)
        {
           return await _dbContext.BlogPosts.FindAsync(id.Value);
        }

        public void Update(BlogPost blogPost)
        {
            throw new NotImplementedException();
        }
    }
}
