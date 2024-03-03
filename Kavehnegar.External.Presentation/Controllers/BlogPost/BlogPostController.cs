using Microsoft.AspNetCore.Mvc;
using Mapster;
using Kavehnegar.Core.Application.BlogPost.Commands.CreateBlogPostCommand;
using Presentation.Controllers;
using Kavehnegar.Shared.Framework.Presentation;
using Kavehnegar.Core.Application.BlogPost.Commands.UpdateBlogPostCommand;
namespace Kavehnegar.External.Presentation.Controllers.BlogPost
{
    public class BlogPostController : ApiController
    {
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequest request, CancellationToken cancellationToken)
        {
            var command = request.Adapt<CreateBlogPostCommand>();

            var webinarId = await Sender.Send(command, cancellationToken);

            var result = new CommandResult(isSuccess: true, message: webinarId.ToString(), errorMessage: null);
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdateBlogPost([FromBody] UpdateBlogPostRequest request, CancellationToken cancellationToken)
        {
            var command = request.Adapt<UpdateBlogPostCommand>();

            var webinarId = await Sender.Send(command, cancellationToken);

            var result = new CommandResult(isSuccess: true, message: webinarId.ToString(), errorMessage: null);
            return Ok(result);
        }
    }
}

