using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Core.Domain.BlogPost
{
    public interface IBlogPostRepository
    {
        void Insert(BlogPost blogPost);
        Task<BlogPost> Load(BlogPostId id);
        void Delete(BlogPost blogPost);
        void Update(BlogPost blogPost);
    }
}
