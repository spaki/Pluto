using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Pluto.Repository.Config
{
    internal abstract class DbEntityConfiguration<TEntity> : IDbEntityConfiguration<TEntity> where TEntity : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntity> entity);
        public void Configure(ModelBuilder modelBuilder) => Configure(modelBuilder.Entity<TEntity>());
    }
}
