using Microsoft.EntityFrameworkCore;
using Pluto.Repository.Config;
using Pluto.Repository.Mapping.Common;
using System.Reflection;

namespace Pluto.Repository.Contexts
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.AddAssemblyConfiguration<IEntityMap>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
