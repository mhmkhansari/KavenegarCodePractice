using Kavehnegar.Core.Domain.BlogPost;
using Kavehnegar.Core.Domain.BlogPost.Events;
using Kavehnegar.Core.Domain.User;
using Kavehnegar.Shared.Framework.Application;
using Kavehnegar.Shared.Framework.Infrastructure;
namespace Kavehnegar.Core.Application.BlogPost.Commands.CreateBlogPostCommand
{
    public sealed class CreateBlogPostCommandHandler : ICommandHandler<CreateBlogPostCommand, Guid>
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventBus _eventBus;
        public CreateBlogPostCommandHandler(IBlogPostRepository blogPostRepository, IUnitOfWork unitOfWork, IEventBus eventBus)
        {
            _blogPostRepository = blogPostRepository;
            _unitOfWork = unitOfWork;
            _eventBus = eventBus;
        }

        public async Task<Guid> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
        {
            var blogPostId = new BlogPostId(Guid.NewGuid());
            var blogPostTitle = BlogPostTitle.FromString(request.Title);
            var blogPostDescription = String.IsNullOrEmpty(request.Description) ? BlogPostDescription.NoDescription : BlogPostDescription.FromString(request.Description);
            var authorId = new UserId(request.AuthorId);
            var blogPost = new Domain.BlogPost.BlogPost(blogPostId, blogPostTitle, authorId);
            blogPost.SetDescription(blogPostDescription);
            _blogPostRepository.Insert(blogPost);
            await _unitOfWork.Commit(cancellationToken);
            await _eventBus.PublishAsync(new BlogPostCreated 
            {
                Id = blogPostId,
                Title = blogPostTitle
            },cancellationToken);
            return blogPostId.Value;

        }
    }
}
