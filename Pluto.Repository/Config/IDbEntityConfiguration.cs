using Microsoft.EntityFrameworkCore;

namespace Pluto.Repository.Config
{
    public interface IDbEntityConfiguration
    {
        void Configure(ModelBuilder modelBuilder);
    }
}
