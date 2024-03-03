using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kavehnegar.Core.Domain.BlogPost;
namespace UnitTests.Domain.BlogPost
{
    public class BlogPostIdTests
    {
        [Fact]
        public void Constructor_ThrowsArgumentNullException_ForEmptyGuid()
        {
            // Arrange
            var emptyGuid = Guid.Empty;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new BlogPostId(emptyGuid));
            Assert.Equal("value", exception.ParamName);
            Assert.Contains("BlogPost id cannot be empty", exception.Message);
        }

        [Fact]
        public void ImplicitOperator_ThrowsFormatException_ForWrongGuidFormat()
        {
            // Arrange
            var invalidGuidString = "invalid-guid";

            // Act & Assert
            var exception = Assert.Throws<FormatException>(() => (BlogPostId)invalidGuidString);
            Assert.Contains("Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).", exception.Message);
        }

        [Fact]
        public void Constructor_CreatesBlogPostId_ForValidGuid()
        {
            // Arrange
            var validGuid = Guid.NewGuid();

            // Act
            var blogPostId = new BlogPostId(validGuid);

            // Assert
            Assert.Equal(validGuid, blogPostId.Value);
        }
    }
}
