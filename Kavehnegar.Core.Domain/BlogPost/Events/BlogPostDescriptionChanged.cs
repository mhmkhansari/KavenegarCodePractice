using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Core.Domain.BlogPost.Events
{
    public record BlogPostDescriptionChanged
    {
        public Guid Id { get; init; }
        public string NewValue { get; init; } = string.Empty;
        public string OldValue { get; init; } = string.Empty;
    }
}
