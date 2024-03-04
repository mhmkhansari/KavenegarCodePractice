using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Core.Application.BlogPost.Commands.CreateBlogPostCommand
{
    public sealed class CreateBlogPostCommandValidator : AbstractValidator<CreateBlogPostCommand>
    {
        public CreateBlogPostCommandValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.AuthorId).NotEmpty();
        }
    }
}
