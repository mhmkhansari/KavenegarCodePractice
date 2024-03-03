using Kavehnegar.Shared.Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Core.Application.BlogPost.Commands.CreateBlogPostCommand
{
    public sealed record CreateBlogPostCommand(string Title, string? Description, Guid AuthorId) : ICommand<Guid>;
}
