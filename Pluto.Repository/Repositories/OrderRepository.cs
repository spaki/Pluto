using Pluto.Domain.Interfaces.Repositories;
using Pluto.Domain.Models;
using Pluto.Repository.Contexts;
using Pluto.Repository.Repositories.Common;

namespace Pluto.Repository.Repositories
{
    public class OrderRepository : CrudRepository<Order>, IOrderRepository
    {
        public OrderRepository(MainDbContext context) : base(context)
        {
        }
    }
}
