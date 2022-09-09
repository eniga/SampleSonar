using MicroOrm.Dapper.Repositories;
using SampleSonar.Core.Interfaces;
using SampleSonar.Data.Entities;

namespace SampleSonar.Core.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DapperRepository<User> repo) : base(repo)
        {
        }
    }
}
