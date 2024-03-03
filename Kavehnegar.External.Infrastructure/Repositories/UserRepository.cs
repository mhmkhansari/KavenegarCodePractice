using Kavehnegar.Core.Domain.BlogPost;
using Kavehnegar.Core.Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kavehnegar.External.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly KavehnegarDbContext _dbContext;

        public UserRepository(KavehnegarDbContext dbContext) =>
            _dbContext = dbContext;
        public void Insert(User user)
        {
            _dbContext.Users.AddAsync(user);
        }
    }
}
