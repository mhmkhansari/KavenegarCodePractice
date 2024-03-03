using Kavehnegar.Core.Application.BlogPost.Commands.CreateBlogPostCommand;
using Kavehnegar.Core.Application.User.Commands;
using Kavehnegar.Shared.Framework.Presentation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;

namespace Kavehnegar.External.Presentation.Controllers.User
{
    public class UserController : ApiController
    {
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            var command = request.Adapt<CreateUserCommand>();

            var userId = await Sender.Send(command, cancellationToken);

            var result = new CommandResult(isSuccess: true, message: userId.ToString(), errorMessage: null);
            return Ok(result);
        }
    }
}
