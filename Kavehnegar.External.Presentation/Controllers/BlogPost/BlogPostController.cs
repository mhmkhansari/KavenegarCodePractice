using Microsoft.AspNetCore.Mvc;
using Mapster;
using Kavehnegar.Core.Application.BlogPost.Commands.CreateBlogPostCommand;
using Presentation.Controllers;
using Kavehnegar.Shared.Framework.Presentation;
using Kavehnegar.Core.Application.BlogPost.Commands.UpdateBlogPostCommand;
using Kavehnegar.Core.Application.BlogPost.Queries;
using Kavehnegar.Core.Application.BlogPost.Queries.GetBlogPostByIdQuery;
using Kavehnegar.Core.Application.BlogPost.Commands.DeleteBlogPostCommand;
namespace Kavehnegar.External.Presentation.Controllers.BlogPost
{
    public class BlogPostController : ApiController
    {
        [HttpGet("{Id:guid}")]
        [ProducesResponseType(typeof(BlogPostResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBlogPost(Guid Id, CancellationToken cancellationToken)
        {
            var query = new GetBlogPostByIdQuery(Id);

            var blogPost = await Sender.Send(query, cancellationToken);

            return Ok(blogPost);
        }
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

        [HttpDelete]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> DeleteBlogPost([FromBody] DeleteBlogPostRequest request, CancellationToken cancellationToken)
        {
            var command = request.Adapt<DeleteBlogPostCommand>();

            var webinarId = await Sender.Send(command, cancellationToken);

            var result = new CommandResult(isSuccess: true, message: webinarId.ToString(), errorMessage: null);
            return Ok(result);
        }
    }
}

