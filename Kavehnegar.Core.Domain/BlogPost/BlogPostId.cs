using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kavehnegar.Shared.Framework;
namespace Kavehnegar.Core.Domain.BlogPost
{
    public sealed class BlogPostId : Value<BlogPostId>
    {
        public Guid Value { get; private set; } // Changed setter to private for immutability

        // Ensures that the GUID provided is not empty and sets the value
        public BlogPostId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentNullException(nameof(value), "BlogPost id cannot be empty");

            Value = value;
        }

        // This constructor is protected and not used in the current context
        protected BlogPostId(string adapterId) { }

        // Allows implicit conversion from BlogPostId to Guid
        public static implicit operator Guid(BlogPostId self) => self.Value;

        // Allows implicit conversion from string to BlogPostId, ensuring that string is a valid GUID
        public static implicit operator BlogPostId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "BlogPost id cannot be null or whitespace.");

            if (!Guid.TryParse(value, out Guid guidValue))
                throw new FormatException("Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");

            return new BlogPostId(guidValue);
        }

        public override string ToString() => Value.ToString();
    }
}
