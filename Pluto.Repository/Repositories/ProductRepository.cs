using Pluto.Domain.Interfaces.Repositories;
using Pluto.Domain.Models;
using Pluto.Repository.Contexts;
using Pluto.Repository.Repositories.Common;

namespace Pluto.Repository.Repositories
{
    public class ProductRepository : CrudRepository<Product>, IProductRepository
    {
        public ProductRepository(MainDbContext context) : base(context)
        {
        }
    }
}
