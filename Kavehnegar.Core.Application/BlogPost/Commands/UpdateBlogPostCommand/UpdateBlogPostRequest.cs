using Kavehnegar.Shared.Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Core.Application.BlogPost.Commands.UpdateBlogPostCommand
{
    public sealed record UpdateBlogPostRequest(Guid Id, string Title, string? Description) : ICommand<Guid>;
}
