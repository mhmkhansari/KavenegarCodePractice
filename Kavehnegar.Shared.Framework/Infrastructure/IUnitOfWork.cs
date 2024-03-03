using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.Shared.Framework.Infrastructure
{
    public interface IUnitOfWork
    {
        Task Commit(CancellationToken cancellationToken = default);
    }
}
