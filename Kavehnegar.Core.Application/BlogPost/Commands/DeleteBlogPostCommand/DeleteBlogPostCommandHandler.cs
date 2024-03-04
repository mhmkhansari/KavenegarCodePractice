using Kavehnegar.Core.Domain.BlogPost;
using Kavehnegar.Shared.Framework.Application;
using Kavehnegar.Shared.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Core.Application.BlogPost.Commands.DeleteBlogPostCommand
{
    public sealed class DeleteBlogPostCommandHandler : ICommandHandler<DeleteBlogPostCommand, Guid>
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBlogPostCommandHandler(IBlogPostRepository blogPostRepository, IUnitOfWork unitOfWork)
        {
            _blogPostRepository = blogPostRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
        {
            var blogPostId = new BlogPostId(request.id);
            var currentBlogPost = await _blogPostRepository.Load(blogPostId);
            if (currentBlogPost != null)
            {
                _blogPostRepository.Delete(currentBlogPost);
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
