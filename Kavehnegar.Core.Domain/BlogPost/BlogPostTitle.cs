using Kavehnegar.Shared.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Core.Domain.BlogPost
{
    public class BlogPostTitle : Value<BlogPostTitle>
    {
        public string Value { get; private set; } // Changed to private setter for immutability

        protected BlogPostTitle() { }

        internal BlogPostTitle(string text)
        {
            Validate(text);
            Value = text;
        }

        public static BlogPostTitle FromString(string text)
        {
            Validate(text);
            return new BlogPostTitle(text);
        }

        public static implicit operator string(BlogPostTitle title) => title.Value;

        public static BlogPostTitle NoTitle => new BlogPostTitle();

        private static void Validate(string text)
        {
            if (text == null)
            {
                throw new ArgumentNullException(nameof(text), "Title cannot be null.");
            }

            // Regex pattern to match English and Persian characters, numbers, and spaces
            // Persian characters range in Unicode: [\u0600-\u06FF\uFB8A\u067E\u0686\u06AF]
            var pattern = @"^[\u0600-\u06FF\uFB8A\u067E\u0686\u06AFa-zA-Z0-9\s]{1,20}$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(text, pattern))
            {
                throw new ArgumentException("Title must contain only English and Persian characters, numbers, and spaces, and be at most 20 characters long.");
            }
        }
    }
}
