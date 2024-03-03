using Kavehnegar.Core.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace UnitTests.Domain.User
{
    public class UsernameTests
    {
        [Fact]
        public void FromString_ValidUsername_ShouldSucceed()
        {
            // Arrange
            var validUsername = "Valid_User123";

            // Act
            var username = Username.FromString(validUsername);

            // Assert
            Assert.Equal(validUsername, username.Value);
        }

        [Fact]
        public void FromString_UsernameTooShort_ShouldThrowArgumentException()
        {
            // Arrange
            var shortUsername = "ab";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Username.FromString(shortUsername));
        }

        [Fact]
        public void FromString_UsernameTooLong_ShouldThrowArgumentException()
        {
            // Arrange
            var longUsername = new string('a', 21); // 21 characters long

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Username.FromString(longUsername));
        }

        [Fact]
        public void FromString_UsernameWithInvalidCharacters_ShouldThrowArgumentException()
        {
            // Arrange
            var invalidUsername = "Invalid@Username!";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Username.FromString(invalidUsername));
        }

        [Fact]
        public void FromString_EmptyUsername_ShouldThrowArgumentException()
        {
            // Arrange
            var emptyUsername = "";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Username.FromString(emptyUsername));
        }

        [Fact]
        public void FromString_NullUsername_ShouldThrowArgumentException()
        {
            // Arrange
            string nullUsername = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Username.FromString(nullUsername));
        }
    }
}
