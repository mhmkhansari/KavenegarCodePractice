using Kavehnegar.Core.Domain.BlogPost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kavehnegar.Shared.Framework;
namespace Kavehnegar.Core.Domain.User
{
    internal class UserId : Value<UserId>
    {
        public Guid Value { get; private set; } // Changed setter to private for immutability

        // Ensures that the GUID provided is not empty and sets the value
        public UserId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentNullException(nameof(value), "User id cannot be empty");

            Value = value;
        }

        // This constructor is protected and not used in the current context
        protected UserId(string adapterId) { }

        // Allows implicit conversion from BlogPostId to Guid
        public static implicit operator Guid(UserId self) => self.Value;

        // Allows implicit conversion from string to BlogPostId, ensuring that string is a valid GUID
        public static implicit operator UserId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "BlogPost id cannot be null or whitespace.");

            if (!Guid.TryParse(value, out Guid guidValue))
                throw new FormatException("Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).");

            return new UserId(guidValue);
        }

        public override string ToString() => Value.ToString();
    }
}

