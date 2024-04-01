using Kavehnegar.Core.Domain.BlogPost;
using Kavehnegar.Shared.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Kavehnegar.Core.Application.BlogPost.Commands.CreateBlogPostCommand;
using Kavehnegar.Shared.Framework.Application;
namespace Kavehnegar.Tests.UnitTests.Application.BlogPost
{
    public class CreateBlogPostCommandHandlerTests
    {
        private readonly Mock<IBlogPostRepository> _mockBlogPostRepository;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IEventBus> _mockEventBus;
        private readonly CreateBlogPostCommandHandler _handler;

        public CreateBlogPostCommandHandlerTests()
        {
            _mockBlogPostRepository = new Mock<IBlogPostRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockEventBus = new Mock<IEventBus>();
            _handler = new CreateBlogPostCommandHandler(_mockBlogPostRepository.Object, _mockUnitOfWork.Object, _mockEventBus.Object);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldCreateBlogPostAndReturnBlogPostId()
        {
            // Arrange
            var command = new CreateBlogPostCommand
            (
                Title: "Test Title",
                Description: "Test Description",
                AuthorId: Guid.NewGuid()
            );

            _mockUnitOfWork.Setup(u => u.Commit(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotEqual(Guid.Empty, result);
            _mockBlogPostRepository.Verify(b => b.Insert(It.IsAny<Kavehnegar.Core.Domain.BlogPost.BlogPost>()), Times.Once);
            _mockUnitOfWork.Verify(u => u.Commit(CancellationToken.None), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenEmptyDescription_ShouldUseNoDescriptionValue()
        {
            // Arrange
            var command = new CreateBlogPostCommand
            (
                Title: "Test Title",
                Description: string.Empty, // Empty description
                AuthorId: Guid.NewGuid()
            );

            _mockUnitOfWork.Setup(u => u.Commit(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _mockBlogPostRepository.Verify(b => b.Insert(It.Is<Kavehnegar.Core.Domain.BlogPost.BlogPost>(bp => bp.Description == BlogPostDescription.NoDescription)), Times.Once);
        }
    }
}
