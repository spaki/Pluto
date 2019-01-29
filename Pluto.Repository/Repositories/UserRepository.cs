using Pluto.Domain.Interfaces.Repositories;
using Pluto.Domain.Models;
using Pluto.Repository.Contexts;
using Pluto.Repository.Repositories.Common;

namespace Pluto.Repository.Repositories
{
    public class UserRepository : CrudRepository<User>, IUserRepository
    {
        public UserRepository(MainDbContext context) : base(context)
        {
        }
    }
}
