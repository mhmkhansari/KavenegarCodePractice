using Kavehnegar.Shared.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kavehnegar.Shared.Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Kavehnegar.External.Infrastructure
{
    public class EfCoreUnitOfWork : IUnitOfWork
    {
        private readonly KavehnegarDbContext _dbContext;

        public EfCoreUnitOfWork(KavehnegarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
