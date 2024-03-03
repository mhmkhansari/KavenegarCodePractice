using Kavehnegar.Shared.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Core.Domain.BlogPost
{
    public class BlogPostDescription : Value<BlogPostDescription>
    {
        public string Value { get; private set; }

        protected BlogPostDescription() { }

        internal BlogPostDescription(string text)
        {
            Validate(text);
            Value = text;
        }

        public static BlogPostDescription FromString(string text)
        {
            Validate(text);
            return new BlogPostDescription(text);
        }

        public static implicit operator string(BlogPostDescription title) => title.Value;

        public static BlogPostDescription NoDescription => new BlogPostDescription();

        private static void Validate(string text)
        {

            // Regex pattern to match English and Persian characters, numbers, and spaces
            // Persian characters range in Unicode: [\u0600-\u06FF\uFB8A\u067E\u0686\u06AF]
            var pattern = @"^[\u0600-\u06FF\uFB8A\u067E\u0686\u06AFa-zA-Z0-9\s]{1,200}$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(text, pattern))
            {
                throw new ArgumentException("Title must contain only English and Persian characters, numbers, and spaces, and be at most 200 characters long.");
            }
        }
    }
}
