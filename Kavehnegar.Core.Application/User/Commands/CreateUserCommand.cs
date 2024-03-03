using Kavehnegar.Shared.Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Core.Application.User.Commands
{
    public sealed record CreateUserCommand(string Username) : ICommand<Guid>;
}
