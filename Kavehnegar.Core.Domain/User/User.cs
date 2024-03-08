using Kavehnegar.Shared.Framework.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Core.Domain.User
{
    public sealed class User : AggregateRoot<UserId>
    {
        public Username username { get; private set; }
        public User() { }
        //protected User() { }
        public User(UserId id, Username _username) 
        {
            Id = id;
            username = _username;
        }
        protected override void EnsureValidState()
        {
            //TODO
        }

        protected override void When(object @event)
        {
            //TODO
        }
    }
}
