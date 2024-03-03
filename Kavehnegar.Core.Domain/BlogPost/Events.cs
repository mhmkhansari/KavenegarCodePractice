using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Core.Domain.BlogPost
{
    public static class Events
    {
        public class BlogPostCreated 
        {
            public Guid Id { get; set; }
            public Guid AuthorId { get; set; }
        }
        public class BlogPostUpdated 
        {
            public Guid Id { get; set; }   
        }
        public class DescriptionChanged 
        {
            public string OldValue {  get; set; }
            public string NewValue { get; set; }
        }
    }
}
