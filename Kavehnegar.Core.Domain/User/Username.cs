using Kavehnegar.Shared.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Core.Domain.User
{
    public class Username : Value<Username>
    {
        public string Value { get; private set; }

        protected Username() { }

        internal Username(string username)
        {
            Validate(username);
            Value = username;
        }

        public static Username FromString(string username)
        {
            Validate(username);
            return new Username(username);
        }

        public static implicit operator string(Username username) => username.Value;

        public static Username NoUsername => new Username();

        private static void Validate(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be empty or whitespace.");
            }

            // Regex pattern to match a valid username: English letters, digits, underscores, and hyphens
            var pattern = @"^[a-zA-Z0-9_-]{3,20}$";

            if (!System.Text.RegularExpressions.Regex.IsMatch(username, pattern))
            {
                throw new ArgumentException("Username must contain only English characters, numbers, underscores, or hyphens, and be between 3 and 20 characters long.");
            }
        }
    }
}
