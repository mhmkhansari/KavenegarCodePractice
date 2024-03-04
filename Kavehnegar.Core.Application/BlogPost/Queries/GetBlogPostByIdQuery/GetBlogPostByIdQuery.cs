using Kavehnegar.Shared.Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Core.Application.BlogPost.Queries.GetBlogPostByIdQuery
{
    public sealed record GetBlogPostByIdQuery(Guid BlogPostId) : IQuery<BlogPostResponse>;
}
