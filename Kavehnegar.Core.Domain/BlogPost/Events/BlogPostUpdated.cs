using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Core.Domain.BlogPost.Events
{
    public record BlogPostUpdated
    {
        public Guid Id { get; init; }
        public string Title { get; init; } = string.Empty;
    }
}
