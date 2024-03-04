using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
namespace Kavehnegar.Core.Application.BlogPost.Commands.DeleteBlogPostCommand
{
    public sealed class DeleteBlogPostCommandValidator : AbstractValidator<DeleteBlogPostCommand>
    {
        public DeleteBlogPostCommandValidator()
        {
            RuleFor(x => x.id).NotEmpty();

        }
    }
}
