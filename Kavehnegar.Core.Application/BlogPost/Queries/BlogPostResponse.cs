using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Core.Application.BlogPost.Queries
{
    public sealed record BlogPostResponse(Guid Id, string Title, string? Description, Guid AuthorId, string AuthorName);
}
