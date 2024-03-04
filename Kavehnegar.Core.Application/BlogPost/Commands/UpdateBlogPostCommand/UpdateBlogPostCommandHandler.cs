using Kavehnegar.Core.Domain.BlogPost;
using Kavehnegar.Core.Domain.User;
using Kavehnegar.Shared.Framework.Application;
using Kavehnegar.Shared.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Core.Application.BlogPost.Commands.UpdateBlogPostCommand
{
    public sealed class UpdateBlogPostCommandHandler : ICommandHandler<UpdateBlogPostCommand, Guid>
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBlogPostCommandHandler(IBlogPostRepository blogPostRepository, IUnitOfWork unitOfWork)
        {
            _blogPostRepository = blogPostRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
        {
            var blogPostId = new BlogPostId(request.id);
            var currentBlogPost = await _blogPostRepository.Load(blogPostId);
            if (currentBlogPost != null)
            {
                var blogPostTitle = BlogPostTitle.FromString(request.Title);
                var blogPostDescription = String.IsNullOrEmpty(request.Description) ? BlogPostDescription.NoDescription : BlogPostDescription.FromString(request.Description);
                currentBlogPost.Update(blogPostTitle);
                currentBlogPost.SetDescription(blogPostDescription);
                _blogPostRepository.Update(currentBlogPost);
                await _unitOfWork.Commit(cancellationToken);
                return currentBlogPost.Id.Value;
            }
            else 
            {
                return Guid.Empty;
            }
            
        }
    }
}
