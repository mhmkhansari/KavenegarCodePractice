using Kavehnegar.Core.Domain.BlogPost;
using Kavehnegar.Core.Domain.User;
using Kavehnegar.Shared.Framework.Application;
using Kavehnegar.Shared.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Core.Application.User.Commands
{
    public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userId = new UserId(Guid.NewGuid());
            var username = Username.FromString(request.Username);
            var user = new Domain.User.User(userId, username);
            _userRepository.Insert(user);
            await _unitOfWork.Commit(cancellationToken);
            return userId.Value;

        }
    }
}
