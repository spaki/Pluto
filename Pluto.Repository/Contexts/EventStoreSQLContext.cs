using Microsoft.EntityFrameworkCore;
using Pluto.Repository.Config;
using Pluto.Repository.Mapping.Common;

namespace Pluto.Repository.Contexts
{
    public class EventStoreSQLContext : DbContext
    {
        public EventStoreSQLContext(DbContextOptions<EventStoreSQLContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddAssemblyConfiguration<IEventMap>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
