using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Core.Domain.User
{
    public interface IUserRepository
    {
        void Insert(User user);
    }
}
