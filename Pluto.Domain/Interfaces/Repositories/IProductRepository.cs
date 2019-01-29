using Pluto.Domain.Interfaces.Repositories.Common;
using Pluto.Domain.Models;

namespace Pluto.Domain.Interfaces.Repositories
{
    public interface IProductRepository : ICrudRepository<Product>
    {
    }
}
