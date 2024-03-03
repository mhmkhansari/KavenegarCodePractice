using Kavehnegar.Core.Domain.BlogPost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Domain.BlogPost
{
    public class TitleTests
    {
        [Fact]
        public void CreateTitle_WithValidEnglishCharacters_ShouldSucceed()
        {
            // Arrange
            var validEnglishTitle = "Valid Title 123";

            // Act
            var title = BlogPostTitle.FromString(validEnglishTitle);

            // Assert
            Assert.Equal(validEnglishTitle, title.Value);
        }

        [Fact]
        public void CreateTitle_WithValidPersianCharacters_ShouldSucceed()
        {
            // Arrange
            var validPersianTitle = "عنوان معتبر ۱۲۳";

            // Act
            var title = BlogPostTitle.FromString(validPersianTitle);

            // Assert
            Assert.Equal(validPersianTitle, title.Value);
        }

        [Fact]
        public void CreateTitle_WithNumbersAndSpaces_ShouldSucceed()
        {
            // Arrange
            var validTitle = "Title 1234";

            // Act
            var title = BlogPostTitle.FromString(validTitle);

            // Assert
            Assert.Equal(validTitle, title.Value);
        }

        [Fact]
        public void CreateTitle_LengthExceeding20Characters_ShouldThrowArgumentException()
        {
            // Arrange
            var longTitle = "This is a really long title exceeding the limit";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => BlogPostTitle.FromString(longTitle));
            Assert.Contains("at most 20 characters long", exception.Message);
        }

        [Fact]
        public void CreateTitle_WithInvalidCharacters_ShouldThrowArgumentException()
        {
            // Arrange
            var invalidTitle = "Invalid#Title@!";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => BlogPostTitle.FromString(invalidTitle));
            Assert.Contains("only English and Persian characters, numbers, and spaces", exception.Message);
        }

        [Fact]
        public void CreateTitle_WithNullValue_ShouldThrowArgumentNullException()
        {
            // Arrange
            string nullTitle = null;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => BlogPostTitle.FromString(nullTitle));
            Assert.Contains("Title cannot be null", exception.Message);
        }
    }
}
